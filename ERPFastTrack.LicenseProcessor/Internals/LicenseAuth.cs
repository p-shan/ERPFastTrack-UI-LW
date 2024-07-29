using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;
using Microsoft.Extensions.Primitives;
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

                HttpResponseMessage response = await _httpClient.PostAsync("https://erpfasttrack-backendsvcs.azurewebsites.net/LicenseValidate", httpContent);

                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ValidateResponse>(responseData);
            }
            catch (Exception)
            {

            }

            return res;
        }

        private static readonly char[] chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public string GetDownloadableRequest()
        {
            string id = GetInstanceId();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    result.Append(id + "\n");

                    continue;
                }

                StringBuilder buildStr = new StringBuilder(id.Length);
                Random random = new Random();

                for (int j = 0; j < id.Length; j++)
                {
                    buildStr.Append(chars[random.Next(chars.Length)]);
                }
                result.Append(buildStr.ToString() + "\n");
            }



            return result.ToString();
        }

        public string GetInstanceId()
        {
            var webinstanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID") ?? "12345";
            var instanceId = Environment.GetEnvironmentVariable("WEB_INSTANCE_ID");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development" && string.IsNullOrEmpty(instanceId))
                instanceId = "12345";

            if (string.IsNullOrEmpty(instanceId))
                return webinstanceId;
            else return instanceId;
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
