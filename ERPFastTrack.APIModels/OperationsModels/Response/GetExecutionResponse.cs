using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
    public class GetExecutionResponse : BaseResponse
    {
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public int NumberOfJobs { get; set; }
        public int SuccessfulJobs { get; set; }
        public int FailedJobs { get; set; }
        public string? FailedReason { get; set; }

        public int ExecutionStatus { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public List<JobDetails> JobDetails { get; set; }
    }

    public class JobDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public string? Observations { get; set; }

        public int ProcessedRecords { get; set; }

        public int FailedRecords { get; set; }

        public int TotalRecords { get; set; }

        public string ExternalId { get; set; }

        public string SourceName { get; set; }

        public string SourceDetName { get; set; }

        public string DestName { get; set; }

        public string DestDetName { get; set; }

        public int ProjectType { get; set; }

        public int DestinationType { get; set; }
    }
}
