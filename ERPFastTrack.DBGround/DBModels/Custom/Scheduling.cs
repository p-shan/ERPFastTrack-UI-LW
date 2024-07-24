using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class Scheduling
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int PId { get; set; }
        public int OrgId { get; set; }
        public int ScheduleType { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public int? HourlyMinutes { get; set; }
        public int? MonthDay { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? MonthTime { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? DailyTime { get; set; }
        public Organization Organization { get; set; }
        public Project Project { get; set; }
        public ICollection<SchedulingDetails> SchedulingDetails { get; set; }
        public ICollection<Execution> Executions { get; set; }
    }
}
