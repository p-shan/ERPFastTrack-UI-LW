namespace ERPFastTrack.APIModels.OrgModels.Request
{
    public class AddOrganizationUserRequest
    {
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public string Name { get; set; }
        public int OrgRole { get; set; }
    }
}
