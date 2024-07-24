namespace ERPFastTrack.APIModels.OrgModels.Response
{
	public class OrganizationUsersResult : BaseResponse
    {
        public List<OrganizationUser> Users { get; set; }
    }

    public class OrganizationUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
