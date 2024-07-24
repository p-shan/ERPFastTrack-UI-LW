using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Request
{
    public class TestConnectionRequest : BaseRequest
    {
        public string ConnectionString { get; set; }
    }
}
