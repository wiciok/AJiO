using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IParsingResultPdf
    {
        bool PatternOccured { get; }
        int PatternOccurenciesNumber { get; set; }
    }
}
