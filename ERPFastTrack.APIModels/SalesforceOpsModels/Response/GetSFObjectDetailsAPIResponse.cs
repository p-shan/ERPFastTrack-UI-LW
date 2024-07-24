using ERPFastTrack.APIModels.SalesforceOpsModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.APIModels.SalesforceOpsModels.Response
{
    public class GetSFObjectDetailsAPIResponse : BaseResponse
    {
        public List<FieldData> Data { get; set; }
    }

    public class FieldData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsExternalId { get; set; }
        public bool IsLookup { get; set; }
        public string? LookupName { get; set; }
    }

    public class FieldDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public object Sftype { get; set; }
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
