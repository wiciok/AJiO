using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPlansParserPdf: IFacultyPlansParser
    {
        IParsingResultPdf Parse(Stream file);
    }
}
