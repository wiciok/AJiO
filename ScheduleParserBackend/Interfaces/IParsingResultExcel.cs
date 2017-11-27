using System;
using System.Collections.Generic;

namespace ScheduleParserBackend.Interfaces
{
    public interface IParsingResultExcel
    {
        IList<Tuple<DateTime, DateTime>> ScheduleEntriesList { get; }
    }
}
