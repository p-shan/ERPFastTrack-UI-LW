using ERPFastTrack.APIModels;

namespace ERPFastTrack.APIModels.SalesforceOpsModels.Response
{
    public class GetAllsObjectsAPIResponse : BaseResponse
    {
        public List<SObjects> Data { get; set; }
    }

    public class SFsObjects
    {
        public List<SObjects> sobjects { get; set; }
    }

    public class SObjects
    {
        public string Name { get; set; }
    }
}
