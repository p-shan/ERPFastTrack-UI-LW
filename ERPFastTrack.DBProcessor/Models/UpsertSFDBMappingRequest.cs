using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.DBProcessor.Models
{
    public class UpsertSFDBMappingRequest
    {
        private string id;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id ?? Guid.NewGuid().ToString(); }
            set { id = value; }
        }

        public string ObjectName { get; set; }

        public string TableName { get; set; }

        public string SalesforceId { get; set; }

        public string ConfigurationType { get; private set; } = "SF_DB_ObjectMapping";

        public List<Mapping> Mapping { get; set; }
        public List<string> LookupsToLoad { get; set; }
    }

    public class Mapping
    {
        public string Key { get; set; }
        public ValueMap Value { get; set; }
    }

    public class ValueMap
    {
        public bool ExternalFlag { get; set; }
        public bool LookupFlag { get; set; }
        public string Value { get; set; }
        public string LookupId { get; set; }
        public string LookupName { get; set; }
    }
}
