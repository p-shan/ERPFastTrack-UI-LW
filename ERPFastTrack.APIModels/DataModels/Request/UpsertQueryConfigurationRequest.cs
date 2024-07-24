﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.DataModels.Request
{
	public class UpsertQueryConfigurationRequest : BaseRequest
	{
		public int Id { get; set; }

		public string QueryName { get; set; }

		public string QueryDetails { get; set; }

		public int OrgId { get; set; }
	}
}
