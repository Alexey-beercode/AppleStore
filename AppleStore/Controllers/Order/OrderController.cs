namespace AppleStore.Controllers.Order;

public class OrderController:Controller
{
    private readonly IOrderService _orderService;
    private readonly IDeviceService _deviceService;

    public OrderController(IOrderService orderService, IDeviceService deviceService)
    {
        _orderService = orderService;
        _deviceService = deviceService;
    }
    public async Task<IActionResult> Details(int id)
    {
        BaseResponse < Domain.Entity.Device > response = await _deviceService.GetById(id);
        return View("Details", response.Data);
    }
    
    [HttpGet]
    public async Task<IActionResult> PlaceOrder(int id)
    {
        BaseResponse < Domain.Entity.Device > response = await _deviceService.GetById(id);
        return View(new DeviceOrderViewModel(){Device = response.Data});
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(DeviceOrderViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _orderService.CreateOrder(viewModel.Order);
        }
        else
        {
            return RedirectToRoute("~/Device/Error",new ErrorViewModel(){RequestId = "Неверно введенные данные"});
        }
        return View("FinishOrder");
    }
}