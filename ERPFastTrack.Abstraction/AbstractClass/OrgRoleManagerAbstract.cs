using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Models;

namespace ERPFastTrack.Abstraction.AbstractClass
{
	public class OrgRoleManagerAbstract : IRoleManager
	{
		public bool LoggedInUser { get; set; }
		public bool IsOrgSelected { get; set; }
		public RoleData Role { get; set; }
	}
}
