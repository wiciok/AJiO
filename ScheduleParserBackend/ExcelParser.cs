using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class ExcelParser: IFacultyPlansParserExcel
    {
        public IParsingResultExcel Parse(Stream file, string patternToSearch)
        {
            throw new NotImplementedException();
        }
    }
}
