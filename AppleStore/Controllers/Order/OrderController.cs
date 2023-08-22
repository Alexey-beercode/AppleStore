
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
    
    [Authorize]
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
        DeviceOrderViewModel model = new DeviceOrderViewModel() { Device = response.Data,Order = default};
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(DeviceOrderViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Error : Ошибка валидации данных"); 
            return View("Error", "Неправильно введенные данные");
          
        }
        await _orderService.CreateOrder(viewModel.Order); 
        _logger.LogInformation("Успешное создание заказа");
        return View("FinishOrder");
    }
}