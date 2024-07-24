using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.SourceProcessor.Sources.SQLServer
{
    public class SQLServerSourceRequest
    {
        public string ConnStr { get; set; }
        public string Query { get; set; }
    }
}
