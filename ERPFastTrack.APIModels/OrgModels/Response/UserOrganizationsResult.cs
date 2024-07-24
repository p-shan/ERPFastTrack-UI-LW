namespace ERPFastTrack.APIModels.OrgModels.Response
{
	public class UserOrganizationsResult : BaseResponse
    {
        public List<UserOrganization> Organizations { get; set; }
    }

    public class UserOrganization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrgRole { get; set; }
    }
}
