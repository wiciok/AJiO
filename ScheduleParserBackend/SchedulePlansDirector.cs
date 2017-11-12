using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class SchedulePlansDirector : ISchedulePlansDirector
    {
        private IEnumerable<string> _schedulePlansLinks;
        private IList<Tuple<Stream, ScheduleFileType>> _scheduleFiles;
        private IList<IParsingResultPdf> _parsingResultsPdf;
        //private IEnumerable<IParsingResultExcel> _parsingResultExcel;

        private readonly IFacultyPageParser _facultyPageParser;
        private readonly IScheduleFilesDownloader _scheduleFilesDownloader;
        private readonly IFacultyPlansParserPdf _pdfParser;
        private readonly IFacultyPlansParserExcel _excelParser;

        public SchedulePlansDirector
        (
            IFacultyPageParser facultyPageParser, 
            IScheduleFilesDownloader scheduleFilesDownloader, 
            IFacultyPlansParserPdf pdfParser, 
            IFacultyPlansParserExcel excelParser
        )
        {
            _facultyPageParser = facultyPageParser;
            _scheduleFilesDownloader = scheduleFilesDownloader;
            _excelParser = excelParser;
            _pdfParser = pdfParser;          
            
            _parsingResultsPdf = new List<IParsingResultPdf>();
        }

        public async Task GetSchedulePlansLinks()
        {
            _schedulePlansLinks = await _facultyPageParser.GetSchedulePlansListAsync();
        }

        public async Task GetSchedulePlansFiles()
        {
            _scheduleFiles = await _scheduleFilesDownloader.GetSchedulePlansFiles(_schedulePlansLinks);
        }

        public void ParsePdfs()
        {
            var pdfFiles = _scheduleFiles
                .Where(x => x.Item2.Equals(ScheduleFileType.Pdf))
                .Select(x => x.Item1);

            foreach (var file in pdfFiles)
            { 
                var parsingResult = _pdfParser.Parse(file);
                _parsingResultsPdf.Add(parsingResult);
            }
        }

        public void ParseExcel()
        {
            throw new NotImplementedException();
        }

        public IFinalParsingResult CombineResults()
        {
            throw new NotImplementedException();
        }

    }
}
