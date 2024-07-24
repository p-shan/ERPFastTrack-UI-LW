using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBProcessor.Models
{
    public class SQLColumnsRequest
    {
        public string QueryText { get; set; }
        public DBProcessingType ProcessingType { get; set; }
    }
    public enum DBProcessingType { COLUMNFROMDB, COLUMNFROMSQL }
}
