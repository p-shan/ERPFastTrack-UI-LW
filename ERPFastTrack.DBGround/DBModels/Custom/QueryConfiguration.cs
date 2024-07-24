namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class QueryConfiguration
	{
		public int Id { get; set; }

		public string QueryName { get; set; }

		public string QueryDetails { get; set; }

		public int OrgId { get; set; }
        public int DbConnId { get; set; }
        public DatabaseConnection DatabaseConnection { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}

