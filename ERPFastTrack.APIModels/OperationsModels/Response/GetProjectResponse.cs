using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
	public class GetProjectResponse : BaseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DestConnName { get; set; }
		public string DestConnUrl { get; set; }
		public string SourceConnName { get; set; }
		public string SourceConnString { get; set; }
		public int ProjectTypeId { get; set; }
		public int DestinationTypeId { get; set; }
        public int OrgId { get; set; }
        public int SourceConnId { get; set; }
        public int DestConnId { get; set; }
    }
}
