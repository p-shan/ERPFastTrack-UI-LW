using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class Execution
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public Organization Organization { get; set; }
        public int Status { get; set; }
        public DateTime ScheduledAt { get; set; }
        public int ScheduleId { get; set; }
        public string? FailedReason { get; set; }
        public Scheduling Scheduling { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public ICollection<ExecutionDetails> ExecutionDetails { get; set; }
    }
}
