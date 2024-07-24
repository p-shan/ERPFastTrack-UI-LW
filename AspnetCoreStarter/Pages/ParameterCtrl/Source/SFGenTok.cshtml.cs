using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.ParameterCtrl
{
    [Authorize]
    [LicenseAuthorize]
    public class SFGenTok : PageModel
    {
        private readonly ERPFastTrackUIContext context;

        [BindProperty]
        public SalesforceConnection SfConnModel { get; set; }
        //public ParameterCntrlController Ctrl { get; }

        public SFGenTok(ERPFastTrackUIContext context)
        {
            this.context = context;
            //Ctrl = ctrl;
        }

        public IActionResult OnGet()
        {
            return Redirect("https://ias-demo-dev-ed.develop.my.salesforce.com/services/oauth2/authorize?client_id=3MVG9Rr0EZ2YOVMaF_OQCb6aZnrsa3ljB2Yo_76ef4N5Wpg227inwkxjiBh0gzlHLIMAEQDmub.z8RZirMaZk&redirect_uri=https://localhost:7157/signin-salesforce&response_type=code&state=hola");
        }

        public void OnPost()
        {
        }
    }
}
