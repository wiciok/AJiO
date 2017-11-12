using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class PdfParser: IFacultyPlansParserPdf
    {
        private readonly string _patternToSearch;

        public PdfParser(string patternToSearch)
        {
            _patternToSearch = patternToSearch;
        }

        public IParsingResultPdf Parse(Stream file)
        {
            var pdfReader = new PdfReader(file);
            var document = new PdfDocument(pdfReader);
            var numberOfPages = document.GetNumberOfPages();

            var parsingResult = new ParsingResultPdf();

            for (var i = 1; i <= numberOfPages; i++)
            {
                var text = PdfTextExtractor.GetTextFromPage(document.GetPage(i));
                parsingResult.PatternOccurenciesNumber += GetNumberOfPatternOccurencies(text);
            }

            return parsingResult;
        }

        private int GetNumberOfPatternOccurencies(string text)
        {
            var number = 0;

            if (!text.Contains(_patternToSearch))
                return number;



            return number;
        }
    }
}
