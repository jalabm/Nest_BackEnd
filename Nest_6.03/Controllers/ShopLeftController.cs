using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest_6._03.Controllers
{
    public class ShopLeftController : Controller
    {
        private readonly AppDbContext _context;

        public ShopLeftController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products= await _context.products.
                Include(x=>x.Category)
                .Include(x=>x.ProductImgs)
                .OrderByDescending(x => x.Id).Take(20).ToListAsync();
            var categories = await _context.categories.Include(x => x.Products).ToListAsync();
            ProductVm productVm = new ProductVm()
            {
                Products = products,
                Categories = categories
            };
            return View(productVm);
        }
    }
}

