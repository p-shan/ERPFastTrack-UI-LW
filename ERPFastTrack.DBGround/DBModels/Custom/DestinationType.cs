namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class DestinationType
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Project> Projects { get; set; }
	}
}
