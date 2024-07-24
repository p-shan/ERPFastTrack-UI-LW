using ERPFastTrack.DBGround.DBModels.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.LicenseProcessor.Internals
{
    public class LicenseExistsResponse
    {
        public License License { get; set; }
        public Organization Organization { get; set; }
        public bool Exists { get; set; }
    }
}
