using Microsoft.AspNetCore.Mvc;
using Nest_6._03.Data;

namespace Nest_6._03.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{

    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {

        var products = _context.products.ToList();
        return View(products);
        
    }
}

