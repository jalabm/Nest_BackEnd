using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;

namespace Nest_6._03.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _context.products.
            Include(x => x.Category)
            .Include(x => x.ProductImgs)
            .Include(x => x.Vendor)
            .Where(x => !x.SoftDelete)
            .OrderByDescending(x => x.Id).Take(20).ToListAsync();
        var categories = await _context.categories.Include(x => x.Products).Where(x => !x.SoftDelete).ToListAsync();
        ProductVm productVm = new ProductVm()
        {
            Products = products,
            Categories = categories
        };
        return View(productVm);
    }

    public IActionResult ProductCategoryFilter(int? id)
    {
        return ViewComponent("HomeProduct", new { categoryId = id });
    }

}

