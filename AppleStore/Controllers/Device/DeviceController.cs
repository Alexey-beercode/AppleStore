namespace AppleStore.Controllers.Device;

public class DeviceController : Controller
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public async Task<IActionResult> GetDevices()
    {
        BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices();
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel() { RequestId = "Ошибка получения данных" });
    }

    public async Task<IActionResult> GetDeviceById(int id)
    {
        BaseResponse <Domain.Entity.Device> response= await _deviceService.GetById(id);
        if (response.StatusCode!=HttpStatusCode.OK)
        {
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
            return RedirectToAction("Error");
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Save(DeviceViewModel model)
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
}