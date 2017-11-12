using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class SchedulePlansDirector : ISchedulePlansDirector
    {
        private IEnumerable<string> _schedulePlansLinks;
        private IList<Tuple<Stream, ScheduleFileType>> _scheduleFiles;

        public SchedulePlansDirector
            (
                IFacultyPageParser facultyPageParser, 
                IScheduleFilesDownloader scheduleFilesDownloader, 
                IFacultyPlansParser pdfParser, 
                IFacultyPlansParser excelParser
            )
        {
            FacultyPageParser = facultyPageParser;
            ScheduleFilesDownloader = scheduleFilesDownloader;
            ExcelParser = excelParser;
            PdfParser = pdfParser;           
        }

        public IFacultyPageParser FacultyPageParser { get; }
        public IScheduleFilesDownloader ScheduleFilesDownloader { get; }
        public IFacultyPlansParser ExcelParser { get; }
        public IFacultyPlansParser PdfParser { get; }



        public async Task GetSchedulePlansLinks()
        {
            _schedulePlansLinks = await FacultyPageParser.GetSchedulePlansListAsync();
        }

        public async Task GetSchedulePlansFiles()
        {
            _scheduleFiles = await ScheduleFilesDownloader.GetSchedulePlansFiles(_schedulePlansLinks);
        }
    }
}
