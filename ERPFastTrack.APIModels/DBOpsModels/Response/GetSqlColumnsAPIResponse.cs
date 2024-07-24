using Newtonsoft.Json.Linq;

namespace ERPFastTrack.APIModels.DBOpsModels.Response
{
    public class GetSqlColumnsAPIResponse
    {
        public string ErrMessage { get; set; }
        public bool Status { get; set; }
        public List<string> Columns { get; set; }
    }
}
