using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class CellData
    {
        public CellData(ICell cell, DateTime startTime, DateTime endTime)
        {
            Cell = cell;
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime StartTime;
        public DateTime EndTime;
        public ICell Cell;
    }

    public class ParsingResultExcel : IParsingResultExcel
    {
        public IList<CellData> CellsList;
    }

    public class ExcelParser : IFacultyPlansParserExcel
    {
        private IList<ICell> _cellsWithPattern;
        private ISheet _workingSheet;
        private int _columnWithHoursIndex = 2;
        private readonly TimeSpan _oneCellNegativeTimeSpan = TimeSpan.FromMinutes(-15);
        private readonly TimeSpan _oneCellTimeSpan = TimeSpan.FromMinutes(15);

        public IParsingResultExcel Parse(Stream file, string patternToSearch)
        {
            var workbook = WorkbookFactory.Create(file);
            _workingSheet = workbook.GetSheetAt(0);

            _cellsWithPattern = SearchAllCellsForPattern(patternToSearch);

            var list = _cellsWithPattern.Select(delegate (ICell cell)
            {
                var startTime = GetStartTimeForCell(cell);
                return new CellData(cell, startTime, GetEndTimeForCell(cell, startTime));
            });


            return null;
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

            return cellsWithPattern;
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
                SearchForNextFilledHourCell(cell).Deconstruct(out var filledHourCell, out var cellSpan);
                startTime = ParseHourCellToDateTime(filledHourCell.StringCellValue);
                for (var i = 0; i < cellSpan; i++)
                {
                    startTime.Add(_oneCellNegativeTimeSpan);
                }
            }

            return startTime;
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
            } while (!string.IsNullOrEmpty(currentCheckedCell.StringCellValue));

            return new Tuple<ICell, int>(currentCheckedCell, span);
        }

        private DateTime GetEndTimeForCell(ICell cell, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        private DateTime ParseHourCellToDateTime(string text)
        {
            text = text.Replace('.', ':');
            text = text.PadLeft(5, '0');
            return DateTime.ParseExact(text, "HH:mm", CultureInfo.CurrentCulture);
        }
    }
}
