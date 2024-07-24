using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class FileSourceDetails
    {
        public int Id { get; set; }
        public string FileSourceDetailName { get; set; }
        public string FileName { get; set; }
        public string Delimiter { get; set; }
        public string DateFieldFormat { get; set; }
        public string TimeFieldFormat { get; set; }
        public string ArchiveFileName { get; set; }
        public bool HasHeader { get; set; }
        public int FsConnId { get; set; }
        public FileSourceConnection FileSourceConnection { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
