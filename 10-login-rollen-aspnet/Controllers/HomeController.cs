using Microsoft.AspNetCore.Mvc;

namespace LoginRollen.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
