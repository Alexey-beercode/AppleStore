using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleStore.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
    
    public async Task<IActionResult> Index()
    {
        await _signInManager.SignOutAsync();
        return RedirectToRoute("default",new {area="",controller="Home",action="Catalog",type=-1});
    }
}