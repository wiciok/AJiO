using ScheduleParserBackend.Factories.Interfaces;

namespace ScheduleParserBackend.Factories
{
    public static class ParserAbstractFactory
    {
        public static IExcelParserFactory GetExcelParserFactory => new ExcelParserFactory();
        public static IPdfParserFactory GetPdfarserFactory => new PdfParserFactory();
        public static IFacultyPageParserFactory GetFacultyPageParserFactory => new FacultyPageParserFactory();
        public static IScheduleFilesDownloaderFactory GetScheduleFilesDownloaderFactory => new ScheduleFilesDownloaderFactory();
    }
}
