﻿using AppleStore.Domain.Entity;
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
        bool useCache = false;
        BaseResponse<IEnumerable<Order>> response = await _orderService.GetOrders(useCache);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",$"{response.Description}");
        }

        List<DeviceOrderViewModel> models = new List<DeviceOrderViewModel>();
        foreach (var order in response.Data)
        {
            List<Device> devices = new List<Device>();
            string[] ids = order.DevicesId.Split(',');
            foreach (var id in ids)
            {
                if (int.TryParse(id, out int value))
                { 
                    var device = ((await _deviceService.GetById(value)).Data);
                    if (device != null)
                    {
                        devices.Add(device);
                    }
                    
                }
            }
            models.Add(new DeviceOrderViewModel(){Devices = devices,Order = order});
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
        return RedirectToAction("GetOrders");
    }

    public async Task<IActionResult> Edit(int id)
    {
        BaseResponse<Order> response = await _orderService.GetById(id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",$"{response.Description}");
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Order order)
    {
        BaseResponse<Order> response = await _orderService.Edit(order);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",response.Description);
        }
        return RedirectToAction("GetOrders");
    }
}