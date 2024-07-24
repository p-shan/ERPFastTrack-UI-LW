using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
    public class GetSchedulingResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ScheduleType { get; set; }
        public int PId { get; set; }
        public int ProjectTypeId { get; set; }
        public int DestinationTypeId { get; set; }
        public int OrgId { get; set; }
        public bool IsActive { get; set; }
        public int ExecutionStatus { get; set; }
        public int NoOfExecutions { get; set; }
        public List<JobDetail> JobDetails { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? HourlyMinutes { get; set; }
        public int? MonthDay { get; set; }
        public TimeSpan? MonthTime { get; set; }
        public TimeSpan? DailyTime { get; set; }
    }

    public class JobDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceName { get; set; }
        public string DestName { get; set; }
    }
}
