namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class FileSourceConnection
    {
		public int Id { get; set; }
		public string Name { get; set; }
        public string FileLocation { get; set; }
        public string ArchiveLocation { get; set; }
        public int OrgId { get; set; }
		public Organization Organization { get; set; }
		public ICollection<Project> Projects { get; set; }
        public ICollection<FileSourceDetails> FileSourceDetails { get; set; }
    }
}
