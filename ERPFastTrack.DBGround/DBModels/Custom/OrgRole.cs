using System.Text.Json.Serialization;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class OrgRole
	{

		public int Id { get; set; }
		public string RoleName { get; set; }

		// Navigation property for the one-to-one relationship

		public ICollection<OrgUsersRelationship> OrgUsersRelationship { get; set; }
	}
}
