using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRollen.Models;

namespace LoginRollen.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    { _userManager = um; _roleManager = rm; }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var userRoles = new Dictionary<string, IList<string>>();
        foreach (var u in users) userRoles[u.Id] = await _userManager.GetRolesAsync(u);
        ViewBag.UserRoles = userRoles;
        ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        return View(users);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null) { user.IsActive = !user.IsActive; await _userManager.UpdateAsync(user); }
        return RedirectToAction(nameof(Index));
    }
}
