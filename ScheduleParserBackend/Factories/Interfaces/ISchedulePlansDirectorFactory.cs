using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories.Interfaces
{
    public interface ISchedulePlansDirectorFactory
    {
        ISchedulePlansDirector GetSchedulePlansDirector();
    }
}
