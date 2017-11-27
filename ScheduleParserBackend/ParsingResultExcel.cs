using System;
using System.Collections.Generic;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    internal class ParsingResultExcel : IParsingResultExcel
    {
        public IList<Tuple<DateTime, DateTime>> ScheduleEntriesList { get; set; }
    }
}
