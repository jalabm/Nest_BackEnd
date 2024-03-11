using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nest_6._03.Data;

namespace Nest_6._03.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

}

