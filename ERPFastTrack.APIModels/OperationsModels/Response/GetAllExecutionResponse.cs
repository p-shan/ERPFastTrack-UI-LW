using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
    public class GetAllExecutionResponse : BaseResponse
    {
        public List<GetExecutionResponse> Data { get; set; }
    }
}
