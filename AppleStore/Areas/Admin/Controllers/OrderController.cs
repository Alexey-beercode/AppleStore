using AppleStore.Domain.Entity;
using AppleStore.Domain.ViewModels;

namespace AppleStore.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    private readonly IDeviceService _deviceService;

    public OrderController(ILogger<OrderController> logger,IOrderService orderService, IDeviceService deviceService)
    {
        _logger = logger;
        _orderService = orderService;
        _deviceService = deviceService;
    }

    public async Task<IActionResult> GetOrders()
    {
        bool useCache = true;
        BaseResponse<IEnumerable<Order>> orders = await _orderService.GetOrders(useCache);
        BaseResponse<IEnumerable<Device>> devices = await _deviceService.GetDevices(useCache);
        if (orders.StatusCode != HttpStatusCode.OK || devices.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {orders.Description}");
            _logger.LogError($"Error : {devices.Description}");
            return View("Error",$"{orders.Description}{devices.Description}");
        }

        /*IEnumerable<DeviceOrderViewModel> viewModels = new List<DeviceOrderViewModel>();
        foreach (var order in orders)
        {
            
            DeviceOrderViewModel viewModel = new DeviceOrderViewModel
            {
                Device = device,
                Order = orderForDevice,
            };

            deviceOrderViewModels.Add(viewModel);
        }
        */
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        BaseResponse<Order> response = await _orderService.GetById(id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",response.Description);
        }

        return View(response.Data);
    }
}