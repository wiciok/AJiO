using System;

namespace ScheduleParserBackend.OutputModel
{
    public enum ScheduleType
    {
        Single = 0,
        EveryDay = 1,
        EveryHour = 2,
        EveryDayOfWeek = 3
    }

    public class ACSchedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? ACSettingGuid { get; set; }
        public ScheduleType ScheduleType { get; set; }
    }
}
