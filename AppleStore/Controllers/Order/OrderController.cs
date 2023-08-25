
using AppleStore.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        var Devices = new List<Domain.Entity.Device>();
        Devices.Add(response.Data);
        string devicesIds = string.Join(",",Devices.Select(device => device.Id).ToArray());
        DeviceOrderViewModel model = new DeviceOrderViewModel() {Devices = Devices,Order = new Domain.Entity.Order(){DevicesId = devicesIds}};
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(DeviceOrderViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            foreach (var key in ModelState.Keys)
            {
                var fieldState = ModelState[key];
                if (fieldState.ValidationState == ModelValidationState.Invalid)
                {
                    var errors = fieldState.Errors;
                    foreach (var error in errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        _logger.LogError(errorMessage);
                    }
                }
            }
            return View("Error", "Неправильно введенные данные");
        }
        await _orderService.CreateOrder(viewModel.Order);
        _logger.LogInformation("Успешное создание заказа");
        return View("FinishOrder");
    }
}