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
                .Include(x => x.Vendor)
                .OrderByDescending(x => x.Id).Take(20).ToListAsync();
            var categories = await _context.categories.Include(x => x.Products).ToListAsync();
            ProductVm productVm = new ProductVm()
            {
                Products = products,
                Categories = categories
            };
            return View(productVm);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.products.
                Include(x => x.Category)
                .Include(x => x.ProductImgs)
                .Include(x => x.Vendor).FirstOrDefaultAsync(x => x.Id == id);

            var products = await _context.products.
                Include(x => x.Category).ToListAsync();


            if (product == null) return NotFound();
            var categories = await _context.categories.Include(x => x.Products).Where(x => !x.SoftDelete).ToListAsync();
            ProductVm productVm = new ProductVm()
            {
                Product = product,
                Categories = categories
            };
            return View(product);
        }

    }
}

