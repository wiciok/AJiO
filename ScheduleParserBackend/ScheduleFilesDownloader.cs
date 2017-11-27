using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend
{
    public class ScheduleFilesDownloader: IScheduleFilesDownloader
    {
        public async Task<IList<Tuple<Stream, ScheduleFileType>>> GetSchedulePlansFiles(IEnumerable<string> schedulePlansLinks)
        {
            var schedulePlansFiles = new List<Tuple<Stream, ScheduleFileType>>();
            foreach (var link in schedulePlansLinks)
            {
                try
                {
                    HttpResponseMessage result;
                    using (var client = new HttpClient { Timeout = new TimeSpan(0, 0, 10) })
                    {
                        result = await client.GetAsync(link);
                    }

                    ScheduleFileType fileType;
                    Stream content;

                    switch (result.Content.Headers.ContentType.MediaType)
                    {
                        case "application/vnd.ms-excel":
                            fileType = ScheduleFileType.Xls;
                            content = await result.Content.ReadAsStreamAsync();
                            break;

                        case "application/pdf":
                            fileType = ScheduleFileType.Pdf;
                            content = await result.Content.ReadAsStreamAsync();
                            break;

                        default:
                            throw new InvalidDataException("Unexpected file type");
                    }

                    schedulePlansFiles.Add(new Tuple<Stream, ScheduleFileType>(content, fileType));
                }

                catch (Exception e)
                {
                    Console.WriteLine();
                    throw;
                }               
            }
            return schedulePlansFiles;
        }
    }
}
