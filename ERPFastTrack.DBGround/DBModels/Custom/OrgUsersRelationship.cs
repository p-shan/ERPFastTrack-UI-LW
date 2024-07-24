using ERPFastTrack.DBGround.DBModels.Identity;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class OrgUsersRelationship
	{
		public int Id { get; set; }
		public string UserId { get; set; }

		public UserData UserData { get; set; }

		public int OrgId { get; set; }

		public Organization Organization { get; set; }

		// Foreign key property for the one-to-one relationship
		public int RoleId { get; set; }

		// Navigation property for the one-to-one relationship
		public OrgRole Role { get; set; }
	}
}
