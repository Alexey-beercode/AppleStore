using AppleStore.Controllers.Device;

namespace AppleStore.Controllers.Order;

public class OrderController:Controller
{
    private readonly IOrderService _orderService;
    private readonly IDeviceService _deviceService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, IDeviceService deviceService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _deviceService = deviceService;
        _logger = logger;
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(int id)
    {
        BaseResponse < Domain.Entity.Device > response = await _deviceService.GetById(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error", response.Description);
        }
        _logger.LogInformation("Успешное получение Девайса из базы данных");
        return View("Details", response.Data);
    }
    
    [HttpGet]
    public async Task<IActionResult> PlaceOrder(int id)
    {
        BaseResponse < Domain.Entity.Device > response = await _deviceService.GetById(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error", response.Description);
        }
        _logger.LogInformation("Успешное получение Девайса из базы данных");
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
            _logger.LogError($"Error : Ошибка валидации данных");
            return View("Error", "Неправильно введенные данные");
        }
        _logger.LogInformation("Успешное создание заказа");
        return View("FinishOrder");
    }
}