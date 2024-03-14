using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest_6._03.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.products.
                Include(x => x.Category)
                .Include(x => x.ProductImgs)
                .Include(x=>x.Vendor)
                .OrderByDescending(x => x.Id).Take(20).ToListAsync();
            var categories = await _context.categories.Include(x => x.Products).ToListAsync();
            ProductVm productVm = new ProductVm()
            {
                Products = products,
                Categories = categories
            };
            return View(productVm);
        }
        public async Task<IActionResult> Default(){
            var product = await _context.products.
                Include(x => x.Category)
                .Include(x => x.ProductImgs)
                .Include(x=>x.Vendor).FirstOrDefaultAsync();
            var categories = await _context.categories.Include(x => x.Products).ToListAsync();
            ProductVm productVm = new ProductVm()
            {
                product = product,
                Categories = categories
            };
            return View(productVm);
        }

    }
}

