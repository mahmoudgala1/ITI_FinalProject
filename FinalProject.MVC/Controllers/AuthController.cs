
using FinalProject.MVC.Services.Interfaces;
using FinalProject.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace FinalProject.MVC.Controllers;

public class AuthController : Controller
{
    #region Fields
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    #endregion

    #region Constructors
    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    #endregion

    #region Handle Functions
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new AuthFormViewModel());
    }
    [HttpPost]
    public async Task<IActionResult> Login(AuthFormViewModel model)
    {
        var result = await _authService.Login(model);
        if (!result)
            return View("Index", model);
        var claims = new List<Claim>
        {
            new Claim("Email", model.LoginEmail)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties { IsPersistent = true });

        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> Register(AuthFormViewModel model)
    {
        var userWithSameEmail = await _userService.FindByEmailAsync(model.Email);
        if (userWithSameEmail != null)
        {
            ModelState.AddModelError("Email", "Email is already exists");
            return View("Index", model);
        }
        await _authService.Register(model);
        var claims = new List<Claim>
        {
            new Claim("Email", model.Email) // Assuming you have the user's ID
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties { IsPersistent = true });
        return RedirectToAction("Index", "Home");
    }
    #endregion
}
