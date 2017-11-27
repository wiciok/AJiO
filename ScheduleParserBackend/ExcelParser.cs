using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class ExcelParser : IFacultyPlansParserExcel
    {
        private IList<ICell> _cellsWithPattern;
        private ISheet _workingSheet;
        private int _columnWithHoursIndex = 1;
        private readonly TimeSpan _oneCellTimeSpan = TimeSpan.FromMinutes(15);

        public IParsingResultExcel Parse(Stream file, string patternToSearch)
        {
            var workbook = WorkbookFactory.Create(file);
            _workingSheet = workbook.GetSheetAt(0);

            _cellsWithPattern = SearchAllCellsForPattern(patternToSearch);

            var list = _cellsWithPattern.Select(delegate (ICell cell)
            {
                var startTime = GetStartTimeForCell(cell);
                return new Tuple<DateTime, DateTime>(startTime, GetEndTimeForCell(cell, startTime));
            });


            return new ParsingResultExcel { ScheduleEntriesList = list.ToList() };
        }

        private IList<ICell> SearchAllCellsForPattern(string pattern)
        {
            var cellsWithPattern = new List<ICell>();
            for (var i = _workingSheet.FirstRowNum; i <= _workingSheet.LastRowNum; i++)
            {
                var currentRow = _workingSheet.GetRow(i);
                try
                {
                    var newCells = currentRow.Cells.Where(cell =>
                        cell.CellType.Equals(CellType.String) && cell.StringCellValue.Contains(pattern));
                    cellsWithPattern.AddRange(newCells);
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            return cellsWithPattern.ToList();
        }

        private DateTime GetStartTimeForCell(ICell cell)
        {
            var currentRow = cell.Row;
            var hourCell = currentRow.GetCell(_columnWithHoursIndex);

            DateTime startTime;

            if (!string.IsNullOrEmpty(hourCell.StringCellValue))
            {
                startTime = ParseHourCellToDateTime(hourCell.StringCellValue);
            }
            else
            {
                SearchForNextFilledHourCell(hourCell).Deconstruct(out var filledHourCell, out var cellSpan);
                startTime = ParseHourCellToDateTime(filledHourCell.StringCellValue);
                for (var i = 0; i < cellSpan; i++)
                {
                    startTime = startTime.Add(_oneCellTimeSpan);
                }
            }

            try
            {
                startTime = GetDateWithSpecifiedDayOfWeekAndPreserveTime(startTime, GetDayOfWeek(hourCell));
            }
            catch(Exception e) { }
            

            return startTime;
        }

        private DayOfWeek GetDayOfWeek(ICell hourCell)
        {
            var dayOfWeekDict = new Dictionary<string, DayOfWeek>
            {
                {"poniedziałek", DayOfWeek.Monday},
                {"wtorek", DayOfWeek.Tuesday},
                {"środa", DayOfWeek.Wednesday},
                {"czwartek", DayOfWeek.Thursday},
                {"piątek", DayOfWeek.Friday},
            };

            const int dayOfWeekCellIndex = 0;

            var currentCheckedCell = hourCell.Row.GetCell(dayOfWeekCellIndex);
            
            while (string.IsNullOrEmpty(currentCheckedCell.StringCellValue))
            {
                currentCheckedCell = _workingSheet
                    .GetRow(currentCheckedCell.RowIndex - 1)
                    .GetCell(dayOfWeekCellIndex);
            }
            ;

            var cellVal = currentCheckedCell.StringCellValue.ToLower();
            var result = dayOfWeekDict.SingleOrDefault(x => x.Key.Equals(cellVal));

            //if no valid day of week - return value will be 0 (Sunday)
            return result.Value;
        }

        private static DateTime GetDateWithSpecifiedDayOfWeekAndPreserveTime(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            while (dateTime.DayOfWeek != dayOfWeek)
            {
                dateTime = dateTime.AddDays(1);
            }

            return dateTime;
        }

        //iterates up
        private Tuple<ICell, int> SearchForNextFilledHourCell(ICell emptyHourCell)
        {
            var span = 0;
            var currentCheckedCell = emptyHourCell;

            do
            {
                currentCheckedCell = _workingSheet
                    .GetRow(currentCheckedCell.RowIndex - 1)
                    .GetCell(_columnWithHoursIndex);
                span++;
            } while (string.IsNullOrEmpty(currentCheckedCell.StringCellValue));

            return new Tuple<ICell, int>(currentCheckedCell, span);
        }

        private DateTime GetEndTimeForCell(ICell cell, DateTime startTime)
        {
            var span = 0;
            var currentCheckedCell = cell;
            var cellIndex = cell.ColumnIndex;

            try
            {
                do
                {
                    currentCheckedCell = _workingSheet
                        .GetRow(currentCheckedCell.RowIndex + 1)
                        .GetCell(cellIndex);
                    span++;
                } while (currentCheckedCell.IsMergedCell && string.IsNullOrEmpty(currentCheckedCell.StringCellValue));
            }
            catch (NullReferenceException ex)
            {
                //NPOI incorrectly throws NullReferenceExceptions on GetCell method, when its possible to get problematic hourCell via Cells[index]
                //this is just workaaround for this issue
            }
           
            var endTime = startTime;

            for (var i = 0; i < span; i++)
            {
                endTime = endTime.Add(_oneCellTimeSpan);
            }

            return endTime;
        }

        private static DateTime ParseHourCellToDateTime(string text)
        {
            text = text.Replace('.', ':');
            text = text.PadLeft(5, '0');
            return DateTime.ParseExact(text, "HH:mm", CultureInfo.CurrentCulture);
        }
    }
}
