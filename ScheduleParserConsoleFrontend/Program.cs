using System;
using System.Threading.Tasks;
using ScheduleParserBackend;

namespace ScheduleParserConsoleFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var director = new SchedulePlansDirector(new FacultyPageParser(), new ScheduleFilesDownloader(),  null, null);

            await director.GetSchedulePlansLinks();
            await director.GetSchedulePlansFiles();

            Console.ReadKey();
        }
    }
}
