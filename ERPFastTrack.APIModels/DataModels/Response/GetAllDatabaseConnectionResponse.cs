using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Response
{
	public class GetAllDatabaseConnectionResponse : BaseResponse
	{
		public List<GetDatabaseConnectionResponse> Data { get; set; }
	}
}
