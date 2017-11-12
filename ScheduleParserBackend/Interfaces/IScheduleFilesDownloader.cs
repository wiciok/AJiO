using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleParserBackend.Interfaces
{
    public interface IScheduleFilesDownloader
    {
        Task<IList<Tuple<Stream, ScheduleFileType>>> GetSchedulePlansFiles(IEnumerable<string> schedulePlansLinks);
    }
}
