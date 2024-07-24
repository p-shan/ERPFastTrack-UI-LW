namespace ERPFastTrack.Abstraction.Models.SourceData
{
    public class SourceDataDetails
    {
        public List<SourceFieldDetail> Fields { get; set; }
    }

    public class SourceFieldDetail
    {
        public string FieldName { get; set; }
        public string FieldId { get; set; }
        public string FieldType { get; set; }
        public string SubDetail { get; set; }
        public string SampleValue { get; set; }
    }
}
