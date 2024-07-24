using ERPFastTrack.APIModels;

namespace ERPFastTrack.APIModels.SalesforceOpsModels.Response
{
    public class GetSFObjectDetailsInternalAPIResponse : BaseResponse
    {
        public SFObjectDetails ObjectDetails { get; set; }
    }
    public class SFObjectDetails
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public string Id { get { return Name.Trim(); } }
        public string Name { get; set; }
        public string Sftype { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public bool IsPrimaryKey { get; set; }
        public int Length { get; set; }
        public bool IsNullable { get; set; }
        public bool ExternalId { get; set; }
        public bool Nillable { get; set; }
        public bool Custom { get; set; }
        public bool Createable { get; set; }
        public string SoapType { get; set; }
        public bool DefaultedOnCreate { get; set; }
        public List<string> ReferenceTo { get; set; }
    }
}
