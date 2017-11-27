using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories.Interfaces
{
    public interface IPdfParserFactory
    {
        IFacultyPlansParserPdf GetPdfParser();
    }
}
