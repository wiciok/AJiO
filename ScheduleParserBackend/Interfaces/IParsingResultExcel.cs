using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IParsingResultExcel
    {
        IList<ScheduleEntry> ScheduleEntriesList { get; }
    }
}
