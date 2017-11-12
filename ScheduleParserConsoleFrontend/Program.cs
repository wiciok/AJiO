using System;
using System.Threading.Tasks;
using ScheduleParserBackend;

namespace ScheduleParserConsoleFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string pattern = "F112";

            var director = new SchedulePlansDirector(new FacultyPageParser(), new ScheduleFilesDownloader(),  new PdfParser(pattern), null);

            try
            {
                await director.GetSchedulePlansLinks();
                await director.GetSchedulePlansFiles();
                director.ParsePdfs();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

           

            Console.ReadKey();
        }
    }
}
