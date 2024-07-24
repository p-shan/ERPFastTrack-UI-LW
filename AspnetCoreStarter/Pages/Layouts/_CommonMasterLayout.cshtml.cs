using Microsoft.AspNetCore.Mvc.RazorPages;

public class LayoutPageModel : PageModel
{
  public void OnGet()
  {
    TempData["appName"] = "SomeValue";
    // You can set additional TempData keys or perform other setup here
  }
}
