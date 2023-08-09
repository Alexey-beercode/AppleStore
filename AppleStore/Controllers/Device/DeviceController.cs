using System.Collections;
using AppleStore.Domain.DeviceType;
using Microsoft.Extensions.Caching.Memory;

namespace AppleStore.Controllers.Device;

public class DeviceController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly ILogger<DeviceController> _logger;
    private readonly IMemoryCache _cache;
    public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger, IMemoryCache cache)
    {
        _deviceService = deviceService;
        _logger = logger;
        _cache = cache;
    }

    public async Task<IActionResult> GetDevices()
    {
        BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices();
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    public IActionResult Error(string errorMessage)
    {
        return View(errorMessage);
    }
    public async Task<IActionResult> GetDeviceById(int id)
    {
        BaseResponse <Domain.Entity.Device> response= await _deviceService.GetById(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDevice(int id)
    {
        BaseResponse<bool> response= await _deviceService.DeleteDevice(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }
        return RedirectToAction("GetDevices");
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        if (id==0) return View();
        var response =await _deviceService.GetById(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Save(Domain.Entity.Device model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id==0)
            {
                await _deviceService.CreateDevice(model);
            }
            else
            {
                await _deviceService.Edit(model);
            }
        }
        
        return RedirectToAction("GetDevices");
    }

    public async Task<IActionResult> Catalog(int type)
    {
        _cache.TryGetValue("AllDevices", out IEnumerable<Domain.Entity.Device>? devices);
        _logger.LogInformation("Получение всех девайсов из кэша");
        if (devices == null)
        {
            BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices();
            if (response.StatusCode!=HttpStatusCode.OK)
            {
                _logger.LogError($"Error : {response.Description}");
                return View("Error", response.Description);
            }

            devices = response.Data;
            _cache.Set("AllDevices", devices,
                new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
            _logger.LogInformation("Все девайсы добавлены в кэш");
        }
        if(type==-1) 
        {
            return View(devices);
        }
        return View(devices.Where(device=>device.Type==(DeviceType)type).ToList());
    }
}