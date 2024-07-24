using ERPFastTrack.Common.Static;

namespace ERPFastTrack.Abstraction.Models
{
	public class RoleData
	{
		public int Id { get; set; }
		public RolesEnum Role { get; set; }
		public string RoleName { get; set; }
		public string OrgName { get; set; }
		public string UserId { get; set; }
		public string UserEmail { get; set; }
		public int OrgId { get; set; }
		public string UserFullName { get; set; }
        public string LicenseCode { get; set; }
        public bool IsLicenseValid { get; set; }
    }
}
