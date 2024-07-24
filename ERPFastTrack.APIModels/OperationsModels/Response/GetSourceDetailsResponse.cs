using ERPFastTrack.Abstraction.Models.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
    public class GetSourceDetailsResponse : BaseResponse
    {
        public SourceDataDetails Details { get; set; }
    }
}