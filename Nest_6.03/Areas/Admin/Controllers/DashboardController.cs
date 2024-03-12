using Microsoft.AspNetCore.Mvc;

namespace Nest_6._03.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}

