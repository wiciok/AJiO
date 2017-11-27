using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    internal class ParsingResultPdf: IParsingResultPdf
    {
        public int PatternOccurenciesNumber { get; set; }
        public bool PatternOccured => PatternOccurenciesNumber != 0;
    }
}
