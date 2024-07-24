using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Request
{
	public class UpsertDatabaseConnectionRequest : BaseRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ConnectionString { get; set; }

        public List<UpsertQueryConfigurationRequest> QueryConfigurations { get; set; }

        public int OrgId { get; set; }

	}
}
