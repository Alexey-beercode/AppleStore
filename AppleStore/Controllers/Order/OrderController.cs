using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    
    [HttpPost]
    public async Task<IActionResult> GetOrder(int id)
    {
        BaseResponse < Domain.Entity.Device > response = await _deviceService.GetById(id);
        return View("Details", response.Data);
    }
}