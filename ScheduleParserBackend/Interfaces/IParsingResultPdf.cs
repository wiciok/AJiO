namespace ScheduleParserBackend.Interfaces
{
    public interface IParsingResultPdf
    {
        bool PatternOccured { get; }
        int PatternOccurenciesNumber { get; set; }
    }
}
