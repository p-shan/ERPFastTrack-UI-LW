using System;
using System.Collections.Generic;
using ERPFastTrack.API.Internals.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.Dashboard
{
    [LicenseAuthorize]
    public class DashboardModel : PageModel
    {
        public void OnGet() { }
    }
}
