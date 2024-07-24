namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class Job
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int PId { get; set; }
		public int? QueryId { get; set; }
		public string? SObjectName { get; set; }
		public string Mapping { get; set; }
		public int OrgId { get; set; }
        public int? FileSourceDetailId { get; set; }
        public FileSourceDetails FileSourceDetails { get; set; }
        public QueryConfiguration QueryConfiguration { get; set; }
		public Organization Organization { get; set; }
		public Project Project { get; set; }
        public ICollection<SchedulingDetails> SchedulingDetails { get; set; }
        public ICollection<ExecutionDetails> ExecutionDetails { get; set; }
    }
}
