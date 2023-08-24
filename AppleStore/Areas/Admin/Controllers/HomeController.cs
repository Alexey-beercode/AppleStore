using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AppleStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
