using AppleStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AppleStore.Areas.Admin.Controllers;

[Area("Admin")]
public class DeviceController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger)
    {
        _deviceService = deviceService;
        _logger = logger;
    }

    public async Task<IActionResult> GetDevices()
    {
        bool useCache = false;
        BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices(useCache);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }
        return View(response.Data);
    }

    public async Task<IActionResult> Edit(int id)
    {
        BaseResponse<Device> response = await _deviceService.GetById(id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Device device)
    {
        BaseResponse<Device> response = await _deviceService.Edit(device);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",response.Description);
        }
        return RedirectToAction("Index","Home");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Device device)
    {
        BaseResponse<bool> response = await _deviceService.CreateDevice(device);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",response.Description);
        }
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        BaseResponse<bool> response = await _deviceService.DeleteDevice(id);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error",response.Description);
        }
        return RedirectToAction("Index", "Home");
    }
}