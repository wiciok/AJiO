using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class FacultyPageParser : IFacultyPageParser
    {
        private const string FacultySchedulesWebPage = "http://www.fmi.pk.edu.pl/?page=rozklady_zajec.php";
        public FacultyPageParser()
        {

        }

        public async Task<IEnumerable<string>> GetSchedulePlansListAsync()
        {
            HttpResponseMessage result;
            try
            {
                using (var client = new HttpClient { Timeout = new TimeSpan(0, 0, 10) })
                {
                    result = await client.GetAsync(FacultySchedulesWebPage);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine();
                throw;
            }

            var stream = await result.Content.ReadAsStreamAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(stream);

            var allLinkNodes = htmlDocument
                .DocumentNode
                .Descendants()
                .Where(x => x.Name.Equals("a") && x.Attributes["href"] != null);

            var schedulePlansNodes =
                allLinkNodes
                    .Where(x => x.Attributes["href"].Value.Contains("harmonogram"));

            var schedulePlansLinks = schedulePlansNodes.Select(x => x.Attributes["href"].Value);

            return schedulePlansLinks;
        }
    }
}
