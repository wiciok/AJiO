using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class PdfParser : IFacultyPlansParserPdf
    {
        private string _patternToSearch;
        private int _patternOccurenceNumber;

        public IParsingResultPdf Parse(Stream file, string patternToSearch)
        {
            _patternToSearch = patternToSearch;

            var pdfReader = new PdfReader(file);
            var document = new PdfDocument(pdfReader);
            var numberOfPages = document.GetNumberOfPages();

            var parsingResult = new ParsingResultPdf();

            for (var i = 1; i <= numberOfPages; i++)
            {
                var text = PdfTextExtractor.GetTextFromPage(document.GetPage(i));
                _patternOccurenceNumber = 0;
                parsingResult.PatternOccurenciesNumber += GetNumberOfPatternOccurencies(text);
            }

            return parsingResult;
        }

        private int GetNumberOfPatternOccurencies(string text)
        {
            if (!text.Contains(_patternToSearch))
                return _patternOccurenceNumber;

            return ++_patternOccurenceNumber;
        }
    }
}
