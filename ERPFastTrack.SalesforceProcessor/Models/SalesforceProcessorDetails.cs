namespace ERPFastTrack.SalesforceProcessor.Models
{
    public class SalesforceProcessorDetails
    {
        public string Token { get; set; }
        public string ObjectName { get; set; }
        public OperationType Operation { get; set; }
        public string Url { get; set; }
    }

    public enum OperationType { GETALLOBJECTS, GETOBJECTDETAILS }
}
