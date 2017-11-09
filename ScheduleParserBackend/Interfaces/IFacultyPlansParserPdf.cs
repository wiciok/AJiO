using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserPdf: IFacultyPlansParser
    {
        new IParsingResultPdf Parse();
    }
}
