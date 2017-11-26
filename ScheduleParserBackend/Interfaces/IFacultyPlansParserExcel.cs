using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserExcel: IFacultyPlansParser
    {
        IParsingResultExcel Parse(Stream file, string patternToSearch);
    }
}
