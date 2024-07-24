using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Response
{
	public class GetSalesforceDBMappingResponse : BaseResponse
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public int SfConnId { get; set; }
		public string SfConnName { get; set; }
		public string SfConnUrl { get; set; }
		public int OrgId { get; set; }
		public int DbConnId { get; set; }
		public string DbConnName { get; set; }
		public string DbConnString { get; set; }
	}
}
