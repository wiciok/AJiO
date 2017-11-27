using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories
{
    internal class PdfParserFactory: IPdfParserFactory
    {
        public IFacultyPlansParserPdf GetPdfParser()
        {
             return new PdfParser();           
        }
    }
}
