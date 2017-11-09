using System;
using System.Threading.Tasks;
using ScheduleParserBackend;

namespace ScheduleParserConsoleFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var parser = new FacultyPageParser();
            await parser.GetSchedulePlansListAsync();



            Console.WriteLine("Hello World!");
        }
    }
}
