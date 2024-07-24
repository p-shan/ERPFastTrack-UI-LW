using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.SalesforceOpsModels.Request
{
    public class GetSFDBMappingAPIRequest
    {
        public string ObjectName { get; set; }
        public string TableName { get; set; }
    }
}
