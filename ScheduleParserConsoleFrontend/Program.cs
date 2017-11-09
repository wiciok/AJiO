using System;
using System.Threading.Tasks;
using ScheduleParserBackend;

namespace ScheduleParserConsoleFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var director = new SchedulePlansDirector(new FacultyPageParser(), null, null);

            await director.FacultyPageParser.GetSchedulePlansListAsync();

            Console.ReadKey();
        }
    }
}
