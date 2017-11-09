using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserExcel: IFacultyPlansParser
    {
        new IParsingResultExcel Parse();
    }
}
