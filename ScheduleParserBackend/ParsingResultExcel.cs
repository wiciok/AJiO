using System;
using System.Collections.Generic;
using System.Text;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class ParsingResultExcel : IParsingResultExcel
    {
        public IList<ScheduleEntry> ScheduleEntriesList { get; set; }
    }
}
