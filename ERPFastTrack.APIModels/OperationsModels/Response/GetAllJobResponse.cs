﻿using ERPFastTrack.APIModels.DataModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Response
{
	public class GetAllJobResponse : BaseResponse
	{
		public List<GetJobResponse> Data { get; set; }
	}
}