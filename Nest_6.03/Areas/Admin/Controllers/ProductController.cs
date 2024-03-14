using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.Models;

namespace Nest_6._03.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{

    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        List<Product> products = await _context.products
                               .Include(x => x.Category)
                               .Include(x => x.ProductImgs)
                               .Include(x => x.Vendor)
                               .Where(x=>!x.SoftDelete)
                               .ToListAsync();
        return View(products);
        
    }
}

