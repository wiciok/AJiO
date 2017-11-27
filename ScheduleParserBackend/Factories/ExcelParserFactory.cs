using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserBackend.Factories
{
    internal class ExcelParserFactory: IExcelParserFactory
    {
        public IFacultyPlansParserExcel GetExcelParserFActory()
        {
            return new ExcelParser();
        }
    }
}
