using AppleStore.Extensions;
using Microsoft.AspNetCore.Mvc;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AppleStore.Controllers.Cart;

public class CartController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CartController> _logger;
    private readonly IDeviceService _deviceService;

    public CartController(IHttpContextAccessor httpContextAccessor, ILogger<CartController> logger, IDeviceService deviceService)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _deviceService = deviceService;
    }

    public async Task<IActionResult> GetCart()
    {
        List<int>? cart = _httpContextAccessor.HttpContext.Session.GetObject<List<int>>("Cart");
        if (cart != null)
        {
            BaseResponse<IEnumerable<Domain.Entity.Device>> response = await _deviceService.GetDevices(true);
            List<Domain.Entity.Device> devices = response.Data
                .SelectMany(device =>
                    cart.Contains((int)device.Id)
                        ? Enumerable.Repeat(device, cart.Count(id => id == device.Id))
                        : Enumerable.Empty<Domain.Entity.Device>())
                .ToList();
            return View(devices);
        }

        return View(new List<Domain.Entity.Device>());
    } 
    [Authorize]
    public IActionResult AddToCart(int? id)
    {
        List<int> cart = _httpContextAccessor.HttpContext.Session.GetObject<List<int>>("Cart") ?? new List<int>();
        cart.Add((int)id);
        _httpContextAccessor.HttpContext.Session.SetObject("Cart",cart);
        return RedirectToAction("Catalog", "Home");
    }

    public IActionResult RemoveFromCart(int? id)
    {
        List<int> cart = _httpContextAccessor.HttpContext.Session.GetObject<List<int>>("Cart") ?? new List<int>();
        cart.Remove((int)id);
        _httpContextAccessor.HttpContext.Session.SetObject("Cart",cart);
        return RedirectToAction("Catalog", "Home");
    }
}