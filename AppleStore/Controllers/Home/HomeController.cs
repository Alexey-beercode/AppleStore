using AppleStore.Domain.Entity;

namespace AppleStore.Controllers.Home;

public class HomeController : Controller
{
    private readonly IOrderService _orderService;

    public HomeController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IActionResult Index()
    {
        return Redirect("Device/Save");
    }


    [HttpPost]
    public async Task<IActionResult> GetOrder(Domain.Entity.Order order)
    {
        if (ModelState.IsValid)
        {
            if (order.Id == 0)
            {
                await _orderService.CreateOrder(order);
            }
            else
            {
                await _orderService.Edit(order);
            }
        }

        return RedirectToRoute("Device/Save");
    }
}