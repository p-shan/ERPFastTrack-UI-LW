namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class DatabaseConnection
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ConnectionString { get; set; }

		public int OrgId { get; set; }

		public Organization Organization { get; set; }
		public ICollection<Project> Projects { get; set; }
        public ICollection<QueryConfiguration> QueryConfigurations { get; set; }
    }
}
