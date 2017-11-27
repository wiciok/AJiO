using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories.Interfaces
{
    public interface IFacultyPageParserFactory
    {
        IFacultyPageParser GetFacultyPageParser();
    }
}
