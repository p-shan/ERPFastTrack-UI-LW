using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.SalesforceOpsModels.Response
{
    public class GetSFDBMappingAPIResponse
    {
        public string ErrMessage { get; set; }
        public bool Status { get; set; }
        public JToken Data { get; set; }
    }
}
