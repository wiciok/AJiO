using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories.Interfaces
{
    public interface IExcelParserFactory
    {
        IFacultyPlansParserExcel GetExcelParserFActory();
    }
}
