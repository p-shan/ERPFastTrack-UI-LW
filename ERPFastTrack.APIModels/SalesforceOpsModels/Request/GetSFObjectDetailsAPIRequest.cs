namespace ERPFastTrack.APIModels.SalesforceOpsModels.Request
{
    public class GetSFObjectDetailsAPIRequest
    {
        public int SalesforceConnectionId { get; set; }
        public string ObjectName { get; set; }
    }
}
