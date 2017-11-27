using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleParserBackend.Interfaces
{
    public interface ISchedulePlansDirector
    {
        Task<IEnumerable<string>> GetSchedulePlansLinks();
        Task GetSchedulePlansFiles();
        int GetPatternOccurenciesFromPdfParsing();
        void ParseExcel(string pattern);
        void ParsePdfs(string pattern);
        void CollectResultsAndAddOffset(int offset);
        void SerializeAndSaveResults();
    }
}
