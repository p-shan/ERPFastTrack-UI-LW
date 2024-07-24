using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Request
{
	public class UpsertProjectRequest : BaseRequest
	{
		public int Id { get; set; }
		public int DestDetails { get; set; }
        public int Destination { get; set; }
        public int SrcDetails { get; set; }
        public int Source { get; set; }
        public string Name { get; set; }
		public int OrgId { get; set; }

	}
}
