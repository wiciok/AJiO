using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories
{
    internal class FacultyPageParserFactory: IFacultyPageParserFactory
    {
        public IFacultyPageParser GetFacultyPageParser()
        {
            return new FacultyPageParser();
        }
    }
}
