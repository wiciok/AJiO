using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories.Interfaces
{
    public interface IScheduleFilesDownloaderFactory
    {
        IScheduleFilesDownloader GetScheduleFilesDownloader();
    }
}
