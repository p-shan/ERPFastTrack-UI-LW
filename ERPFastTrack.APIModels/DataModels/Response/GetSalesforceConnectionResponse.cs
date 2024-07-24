using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Response
{
	public class GetSalesforceConnectionResponse : BaseResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string? ClientId { get; set; }
		public string? ClientSecret { get; set; }
		public string? TokenEndpoint { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Token { get; set; }
		public DateTime? TokenExpiry { get; set; }
	}
}
