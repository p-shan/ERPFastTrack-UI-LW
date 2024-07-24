using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AspnetCoreStarter.Pages.ParameterCtrl
{
    [Authorize]
    [LicenseAuthorize]
    public class SalesforceConfigModel : PageModel
    {
        private readonly ERPFastTrackUIContext context;

        [BindProperty]
        public SalesforceConnection SfConnModel { get; set; }
        //public ParameterCntrlController Ctrl { get; }

        public SalesforceConfigModel(ERPFastTrackUIContext context)
        {
            this.context = context;
            //Ctrl = ctrl;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var requestData = new
                {
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/api/YourEndpoint", data);
            }
            return Page();
        }

    }
}
