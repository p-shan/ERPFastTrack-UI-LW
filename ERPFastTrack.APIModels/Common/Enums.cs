using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.Common
{
    public enum ScheduleType
    {
        IMMIDIATELY = 1,
        HOURLY,
        DAILY,
        MONTHLY
    }

    public enum ExecutionStatus
    {
        SCHEDULED = 1,
        INPROGRESS,
        COMPLETED
    }
}
