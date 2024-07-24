using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Request
{
	public class UpsertFileSourceConnectionRequest : BaseRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FileLocation { get; set; }
        public string ArchiveLocation { get; set; }

        public List<UpsertFileSourceDetailRequest> FileSourceDetails { get; set; }

        public int OrgId { get; set; }

	}
}
