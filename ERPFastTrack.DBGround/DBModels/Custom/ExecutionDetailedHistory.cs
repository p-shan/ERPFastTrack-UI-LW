using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class ExecutionDetailedHistory
    { 
        public int Id { get; set; }

        public int ExecutionDetailId { get; set; }
        public ExecutionDetails ExecutionDetail { get; set; }
        public string SalesforceId { get; set; }

        public string SObjectName { get; set; }
        public string QueryName { get; set; }
        public string ExternalIdName { get; set; }
        public string ExternalIdValue { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public bool SchemaFailure { get; set; }
        public bool Created { get; set; }
        public string JsonReq { get; set; }
        public string JsonRes { get; set; }
    }
}
