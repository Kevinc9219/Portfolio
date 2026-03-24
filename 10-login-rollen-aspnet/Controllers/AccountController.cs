using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRollen.Models;

namespace LoginRollen.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm)
    { _userManager = um; _signInManager = sm; }

    public IActionResult Login() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded) return RedirectToAction("Index", "Dashboard");
        ModelState.AddModelError("", "Ongeldige inloggegevens.");
        return View(model);
    }

    public IActionResult Register() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Dashboard");
        }
        foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() { await _signInManager.SignOutAsync(); return RedirectToAction("Login"); }

    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login");
        var roles = await _userManager.GetRolesAsync(user);
        ViewBag.Roles = roles;
        return View(user);
    }
}
