using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Response
{
	public class GetFileSourceDetailResponse : BaseResponse
	{
		public int Id { get; set; }
        public string FileSourceDetailName { get; set; }
        public string FileName { get; set; }
        public string Delimiter { get; set; }
        public string DateFieldFormat { get; set; }
        public string TimeFieldFormat { get; set; }
        public string ArchiveFileName { get; set; }
        public bool HasHeader { get; set; }

	}
}
