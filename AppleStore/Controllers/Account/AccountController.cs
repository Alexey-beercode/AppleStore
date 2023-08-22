using AppleStore.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AppleStore.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        //ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        _logger.LogInformation("Хуйня дошла");
        if (ModelState.IsValid)
        {
            _logger.LogInformation("Все норм");
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    //return Redirect(returnUrl ?? "/");
                    return RedirectToAction("Catalog","Device");
                }
            }
            _logger.LogInformation("Все не норм");
        }

        return View("Error","Неверный логин или пароль");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        //ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user!=null)
            {
                return View("Error", "Пользователь уже существует");
            }
            var newUser = new IdentityUser { UserName = model.UserName };
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Зареган");
                await _signInManager.SignInAsync(newUser, isPersistent: false); // Log in the user
                //return Redirect(returnUrl ?? "/");
                return RedirectToAction("Catalog", "Device");
            }
           
        }

        return View("Error", "Неверный логин или пароль");
    }
    
    public async Task<IActionResult> Logout()
    {
        if (!User.Identity.IsAuthenticated)
            return View("Error", "Вы не вошли в аккаунт");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Catalog", "Device");
    }
}