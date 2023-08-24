using AppleStore.Domain.DeviceType;

namespace AppleStore.Controllers.Home;

public class HomeController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IDeviceService deviceService)
    {
        _logger = logger;
        _deviceService = deviceService;
    }

    public async Task<IActionResult> Catalog(int type)
    {
        bool useCache = false;

        BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices(useCache);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error : {response.Description}");
            return View("Error", response.Description);
        }
        
        IEnumerable<Domain.Entity.Device> devices = response.Data;

        if (type == -1)
        {
            return View(devices);
        }
    
        return View(devices.Where(device => device.Type == (DeviceType)type).ToList());
    }
    
}