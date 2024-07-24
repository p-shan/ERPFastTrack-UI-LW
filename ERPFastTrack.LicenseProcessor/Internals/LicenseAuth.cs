using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ERPFastTrack.LicenseProcessor.Internals
{
    public class LicenseAuth : ILicenseAuth
    {
        private readonly ERPFastTrackUIContext _context;

        public LicenseAuth(ERPFastTrackUIContext context)
        {
            this._context = context;
        }

        public async Task<ValidateResponse> Authenticate(string licenseCode, string orgName)
        {
            ValidateResponse res = new();
            try
            {
                var dynamicObj = new { orgName, id = GetInstanceId(), request = licenseCode };

                var _httpClient = new HttpClient();
                HttpContent httpContent = new StringContent(JObject.FromObject(dynamicObj).ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5023/LicenseValidate", httpContent);

                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ValidateResponse>(responseData);
            }
            catch (Exception)
            {

            }

            return res;
        }

        public string GetInstanceId()
        {
            var instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development" && string.IsNullOrEmpty(instanceId))
                instanceId = "12345";

            return instanceId;
        }

        public LicenseExistsResponse LicenseExist()
        {
            LicenseExistsResponse res = new();
            var org = _context.Organizations.FirstOrDefault();
            var license = _context.Licenses.FirstOrDefault();

            if (org != null && license != null)
            {
                res.License = license;
                res.Organization = org;
                res.Exists = true;
                return res;
            }

            return res;
        }
    }

    public class ValidateResponse
    {
        public string OrgName { get; set; }
        public string UserEmail { get; set; }
        public bool IsValid { get; set; }
    }
}
