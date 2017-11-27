using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories
{
    public class SchedulePlansDirectorFactory: ISchedulePlansDirectorFactory
    {
        public ISchedulePlansDirector GetSchedulePlansDirector() => new SchedulePlansDirector
            (ParserAbstractFactory.GetFacultyPageParserFactory.GetFacultyPageParser(),
            ParserAbstractFactory.GetScheduleFilesDownloaderFactory.GetScheduleFilesDownloader(),
            ParserAbstractFactory.GetPdfarserFactory.GetPdfParser(),
            ParserAbstractFactory.GetExcelParserFactory.GetExcelParserFActory());
    }
}
