using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.ParameterCtrl
{
    [Authorize]
    [LicenseAuthorize]
    public class QueryConfigurationsModel : PageModel
    {
        private readonly ERPFastTrackUIContext context;


        // [BindProperty]
        // public QueryConfiguration queryConfModel { get; set; }

        public QueryConfigurationsModel(ERPFastTrackUIContext context)
        {
            this.context = context;
            //Ctrl = ctrl;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            //await Ctrl.PostSalesforceConnection(SfConnModel);
            return Page();
        }
    }
}

