using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginRollen.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class ReportsController : Controller
{
    public IActionResult Index() => View();
}
