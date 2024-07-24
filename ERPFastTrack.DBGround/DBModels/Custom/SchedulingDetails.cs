using ERPFastTrack.DBGround.DBModels.Identity;

namespace ERPFastTrack.DBGround.DBModels.Custom
{
	public class SchedulingDetails
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public int ScheduleId { get; set; }

        public Scheduling Scheduling { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
    }
}
