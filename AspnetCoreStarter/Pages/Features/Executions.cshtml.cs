using ERPFastTrack.API.Internals.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.Features
{
    [Authorize]
    [LicenseAuthorize]
    public class ExecutionModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
