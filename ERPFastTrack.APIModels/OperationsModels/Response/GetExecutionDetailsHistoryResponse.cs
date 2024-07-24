using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
    public class GetExecutionDetailsHistoryResponse : BaseResponse
    {
        public int Id { get; set; }
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
