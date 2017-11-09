using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleParserBackend.Interfaces
{
    public interface IFacultyPageParser
    {
        Task<IEnumerable<string>> GetSchedulePlansListAsync();
    }
}
