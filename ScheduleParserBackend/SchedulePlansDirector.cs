using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScheduleParserBackend.Interfaces;
using ScheduleParserBackend.OutputModel;

namespace ScheduleParserBackend
{
    public class SchedulePlansDirector : ISchedulePlansDirector
    {
        private IEnumerable<string> _schedulePlansLinks;
        private IList<Tuple<Stream, ScheduleFileType>> _scheduleFiles;
        private IList<IParsingResultPdf> _parsingResultsPdf;
        private IList<IParsingResultExcel> _parsingResultsExcel;
        private IEnumerable<ACSchedule> _finalResult;

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
        }

        public async Task<IEnumerable<string>> GetSchedulePlansLinks()
        {
            _schedulePlansLinks = await _facultyPageParser.GetSchedulePlansListAsync();
            return _schedulePlansLinks;
        }

        public async Task GetSchedulePlansFiles()
        {
            _scheduleFiles = await _scheduleFilesDownloader.GetSchedulePlansFiles(_schedulePlansLinks);
        }

        public int GetPatternOccurenciesFromPdfParsing()
        {
            return _parsingResultsPdf.Where(result => result.PatternOccured).Sum(result => result.PatternOccurenciesNumber);
        }

        public void ParsePdfs(string pattern)
        {
            _parsingResultsPdf = new List<IParsingResultPdf>();

            var pdfFiles = _scheduleFiles
                .Where(x => x.Item2.Equals(ScheduleFileType.Pdf))
                .Select(x => x.Item1);

            foreach (var file in pdfFiles)
            {
                var parsingResult = _pdfParser.Parse(file, pattern);
                _parsingResultsPdf.Add(parsingResult);
            }
        }

        public void ParseExcel(string pattern)
        {
            _parsingResultsExcel = new List<IParsingResultExcel>();

            var excelFiles = _scheduleFiles
                .Where(x => x.Item2.Equals(ScheduleFileType.Xls))
                .Select(x => x.Item1);

            foreach (var file in excelFiles)
            {
                var parsingResult = _excelParser.Parse(file, pattern);
                _parsingResultsExcel.Add(parsingResult);
            }
        }

        public void CollectResultsAndAddOffset(int offset)
        {
            var allResults = _parsingResultsExcel.SelectMany(x => x.ScheduleEntriesList);
            var id = 0;

            IList<ACSchedule> list = allResults.Select(x => new ACSchedule
            {
                Id = id++,
                StartTime = x.Item1.AddMinutes(-1 * offset),
                EndTime = x.Item2,
                ScheduleType = ScheduleType.EveryDayOfWeek 
            })
            .ToList();

            _finalResult = list;
        }

        public void SerializeAndSaveResults()
        {
            var pathToFile = @"./" + typeof(ACSchedule).Name + ".json";
            using (StreamWriter file = File.CreateText(pathToFile))
            {
                var serializer = JsonSerializer.Create();
                serializer.Serialize(file, _finalResult);
            }
        }
    }
}
