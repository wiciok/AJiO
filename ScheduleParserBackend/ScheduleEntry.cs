using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;

namespace ScheduleParserBackend
{
    public class ScheduleEntry
    {
        public ScheduleEntry(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime StartTime;
        public DateTime EndTime;
    }
}
