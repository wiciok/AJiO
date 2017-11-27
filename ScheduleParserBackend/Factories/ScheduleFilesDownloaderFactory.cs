using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories
{
    internal class ScheduleFilesDownloaderFactory: IScheduleFilesDownloaderFactory
    {
        public IScheduleFilesDownloader GetScheduleFilesDownloader()
        {
            return new ScheduleFilesDownloader();
        }
    }
}
