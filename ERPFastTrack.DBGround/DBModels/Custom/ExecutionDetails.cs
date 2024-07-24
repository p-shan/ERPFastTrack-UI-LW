using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class ExecutionDetails
    {
        public int Id { get; set; }

        public int ExecutionId { get; set; }
        public Execution Execution { get; set; }


        public int JobId { get;set; }
        public Job Job { get; set; }

        public bool Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public string? Observations { get; set; }

        public int ProcessedRecords { get; set; }

        public int FailedRecords { get; set; }

        public int TotalRecords { get; set; }

        public ICollection<ExecutionDetailedHistory> ExecutionDetailedHistories { get; set; }
    }
}
