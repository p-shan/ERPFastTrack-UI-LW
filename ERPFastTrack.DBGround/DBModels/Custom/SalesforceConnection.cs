namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class SalesforceConnection
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string? ClientId { get; set; }
		public string? ClientSecret { get; set; }
		public string? TokenEndpoint { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
        public DateTime? TokenExpiry { get; set; }

		public int OrgId { get; set; }

		public Organization Organization { get; set; }
		public ICollection<Project> Projects { get; set; }

        }
}
