using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleParserBackend
{
    public interface IFacultyPageParser
    {
        Task<IEnumerable<string>> GetSchedulePlansListAsync();
    }
}
