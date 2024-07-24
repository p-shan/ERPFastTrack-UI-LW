namespace ERPFastTrack.APIModels
{
	public class BaseResponse
	{
		public bool Error { get { return !Status; } }
		public bool Status { get; set; }
		public string ErrorMessage { get; set; }
	}
}
