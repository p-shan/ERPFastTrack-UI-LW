using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
    public class License
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string LicenseCode { get; set; }
        public bool IsValid { get; set; }
        public DateTime LastValidation { get; set; }

        public bool IsValidLicense()
        {
            if (this.IsValid && this.LastValidation > DateTime.Now.AddMinutes(-15))
                return true;
            return false;
        }
    }
}
