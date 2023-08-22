namespace AppleStore.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Policy = "AdminArea")]
public class HomeController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
}