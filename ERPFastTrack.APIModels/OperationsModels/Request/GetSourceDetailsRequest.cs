using ERPFastTrack.Common.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.OperationsModels.Request
{
    public class GetSourceDetailsRequest
    {
        public ProjectTypesEnum SourceType { get; set; }
        public int Id { get; set; }
    }
}
