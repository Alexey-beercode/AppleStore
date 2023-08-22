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
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Пользователь {model.UserName} Авторизовался успешно");
                    return View("Successful","Успешная Авторизация");
                }
            }
            _logger.LogInformation("Авторизация прошла не успешно : пользователя не найдено");
            return View("Error", "Пользователь не найден");
        }

        return View("Error","Неверный логин или пароль");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
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
                return View("Successful","Успешная Регистрация");
            }
           
        }

        return View("Error", "Неверный логин или пароль");
    }
    
    public async Task<IActionResult> Logout()
    {
        if (!User.Identity.IsAuthenticated)
            return View("Error", "Вы не вошли в аккаунт");
            await _signInManager.SignOutAsync();
            return View("Successful","Вы вышли из аккаунта");
    }
}