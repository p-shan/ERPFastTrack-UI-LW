using ERPFastTrack.APIModels.DataModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
	public class GetAllSchedulingResponse : BaseResponse
	{
		public List<GetSchedulingResponse> Data { get; set; }
	}
}
