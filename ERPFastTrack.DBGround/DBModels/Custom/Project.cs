namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }

        public int ProjectTypeId { get; set; }
        public int DestinationTypeId { get; set; }
        public int OrgId { get; set; }
        public int? DbConnId { get; set; }
        public int? SfConnId { get; set; }
        public int? FsConnId { get; set; }

        public Organization Organization { get; set; }
        public ProjectType ProjectType { get; set; }
        public DestinationType DestinationType { get; set; }
        public SalesforceConnection? SalesforceConnection { get; set; }
        public DatabaseConnection? DatabaseConnection { get; set; }
        public FileSourceConnection? FileSourceConnection { get; set; }
        public ICollection<Job> Jobs { get; set; }
		public ICollection<Scheduling> Schedulings { get; set; }

    }
}
