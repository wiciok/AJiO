using System;
using System.Collections.Generic;
using System.Text;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class SchedulePlansDirector: ISchedulePlansDirector
    {
        public SchedulePlansDirector(IFacultyPageParser facultyPageParser, IFacultyPlansParser pdfParser, IFacultyPlansParser excelParser)
        {
            FacultyPageParser = facultyPageParser;
            ExcelParser = excelParser;
            PdfParser = pdfParser;
        }

        public IFacultyPageParser FacultyPageParser { get; }
        public IFacultyPlansParser ExcelParser { get; }
        public IFacultyPlansParser PdfParser { get; }


    }
}
