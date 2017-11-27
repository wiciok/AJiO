using System.IO;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserExcel: IFacultyPlansParser
    {
        IParsingResultExcel Parse(Stream file, string patternToSearch);
    }
}
