using ERPFastTrack.Common.Static;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
	public class GetJobResponse : BaseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int PId { get; set; }
        public int DestDetailId { get; set; }
        public int Destination { get; set; }
        public int SrcDetailId { get; set; }
        public int SrcSubDetailId { get; set; }
        public string SrcSubDetailName { get; set; }
        public int Source { get; set; }
        public int DestSubDetailId { get; set; }
        public string DestSubDetailName { get; set; }
        public JToken Mapping { get; set; }
		public int OrgId { get; set; }

	}
}
