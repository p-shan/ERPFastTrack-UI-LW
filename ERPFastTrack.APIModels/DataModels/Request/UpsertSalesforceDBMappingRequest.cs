using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Request
{
	public class UpsertSalesforceDBMappingRequest : BaseRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int SfConnId { get; set; }
		public int OrgId { get; set; }
		public int DbConnId { get; set; }
	}
}
