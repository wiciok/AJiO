using System;
using System.Collections.Generic;
using System.Text;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    class ParsingResultPdf: IParsingResultPdf
    {
        public int PatternOccurenciesNumber { get; set; }
        public bool PatternOccured => PatternOccurenciesNumber != 0;
    }
}
