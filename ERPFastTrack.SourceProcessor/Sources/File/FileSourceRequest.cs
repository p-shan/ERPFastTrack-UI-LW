using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.SourceProcessor.Sources.File
{
    public class FileSourceRequest
    {
        public string FilePath { get; set; }
        public bool HasHeader { get; set; }
    }
}
