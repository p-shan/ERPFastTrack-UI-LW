using ERPFastTrack.APIModels.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Request
{
    public class UpsertSchedulingRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PId { get; set; }
        public int OrgId { get; set; }
        public int ScheduleType { get; set; }
        public bool IsActive { get; set; }
        public List<int> JobIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? HourlyMinutes { get; set; }
        public int? MonthDay { get; set; }
        public TimeSpan? MonthTime { get; set; }
        public TimeSpan? DailyTime { get; set; }
    }
}
