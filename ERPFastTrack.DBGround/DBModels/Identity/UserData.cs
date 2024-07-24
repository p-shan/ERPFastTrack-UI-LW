using ERPFastTrack.DBGround.DBModels.Custom;
using Microsoft.AspNetCore.Identity;

namespace ERPFastTrack.DBGround.DBModels.Identity
{
	public class UserData : IdentityUser
	{
		public string? FullName { get; set; }

        // Navigation property

        public ICollection<OrgUsersRelationship> Organizations { get; set; }
	}
}
