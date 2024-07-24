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
    public class FileSourceConnectionsModel : PageModel
    {
        private readonly ERPFastTrackUIContext context;

        [BindProperty]
        public DatabaseConnection DbConnModel { get; set; }
        //public ParameterCntrlController Ctrl { get; }

        public FileSourceConnectionsModel(ERPFastTrackUIContext context)
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
    }
}
