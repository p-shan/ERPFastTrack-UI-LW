using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Response
{
	public class GetDatabaseConnectionResponse : BaseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ConnectionString { get; set; }
        public List<GetQueryConfigurationResponse> QueryConfigurations { get; set; }
		public int OrgId { get; set; }

	}
}
