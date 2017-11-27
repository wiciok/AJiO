using System.IO;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserPdf: IFacultyPlansParser
    {
        IParsingResultPdf Parse(Stream file, string patternToSearch);
    }
}
