
using AppleStore.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppleStore.Controllers.Order;

public class OrderController:Controller
{
    private readonly IOrderService _orderService;
    private readonly IDeviceService _deviceService;
    private readonly ILogger<OrderController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderController(IOrderService orderService, IDeviceService deviceService, ILogger<OrderController> logger, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _deviceService = deviceService;
        _logger = logger;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> PlaceOrder(List<int> ids)
    {
        IdentityUser user =await _userManager.GetUserAsync(HttpContext.User);
        BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices(true);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error", response.Description);
        }
        _logger.LogInformation("Успешное получение Девайса из базы данных");
        List<Domain.Entity.Device> devices = response.Data
            .SelectMany(device => ids.Contains((int)device.Id) ? Enumerable.Repeat(device, ids.Count(id => id == device.Id)) : Enumerable.Empty<Domain.Entity.Device>())
            .ToList();
        
        DeviceOrderViewModel model = new DeviceOrderViewModel() {Devices = devices,Order = new Domain.Entity.Order(){DevicesId = string.Join(",",ids.ToArray()),UserId =user.Id,Email = user.Email}};
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
        _httpContextAccessor.HttpContext.Session.Remove("Cart");
        await _orderService.CreateOrder(viewModel.Order);
        _logger.LogInformation("Успешное создание заказа");
        return View("FinishOrder");
    }

    public async Task<IActionResult> GetOrdersHistory(string userId)
    {
        bool useCache = false;
        BaseResponse<IEnumerable<Domain.Entity.Order>> response = await _orderService.GetOrders(useCache);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",$"{response.Description}");
        }

        List<DeviceOrderViewModel> models = new List<DeviceOrderViewModel>();
        foreach (var order in response.Data)
        {
            if (order.UserId == userId)
            {
                List<Domain.Entity.Device> devices = new List<Domain.Entity.Device>();
                string[] ids = order.DevicesId.Split(',');
                foreach (var id in ids)
                {
                    if (int.TryParse(id, out int value))
                    {
                        devices.Add((await _deviceService.GetById(value)).Data);
                    }
                }

                models.Add(new DeviceOrderViewModel() { Devices = devices, Order = order });
            }
        }
        return View(models);
    }

    public async Task<IActionResult> Delete(int orderId)
    {
        BaseResponse < bool > response= await _orderService.DeleteOrder(orderId);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",$"{response.Description}");
        }
        string userId =(await _userManager.GetUserAsync(HttpContext.User)).Id;
        return RedirectToAction("GetOrdersHistory",new {userId=userId});
    }
}