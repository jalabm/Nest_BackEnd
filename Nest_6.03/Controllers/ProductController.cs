using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;
using Newtonsoft.Json;

namespace Nest_6._03.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(ProductSearchVm? vm, int page = 1, int pageSize = 1)
    {
        var products = _context.products.AsQueryable();
        products = products
           .Include(x => x.Category)
           .Include(x => x.ProductImgs)
           .Include(x=>x.Vendor)
           .Skip((page - 1) * pageSize)
           .Take(pageSize);
        var count = GetPageCount(pageSize);
        var categories = await _context.categories.Include(x=>x.Products).ToListAsync();
        PaginateVm paginateVm = new PaginateVm()
        {
            CurrentPage = page,
            TotalPageCount = count,
            Products = await products.ToListAsync(),
            Categories=categories
        };
        return View(paginateVm);
    }


    public int GetPageCount(int pageSize)
    {
        var productCount = _context.products.Count();
        return (int)Math.Ceiling((decimal)productCount / pageSize);
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

    public async Task<IActionResult> AddToCart(int id)
    {
        var existProduct = await _context.products.AnyAsync(x => x.Id == id);
        if (!existProduct) return NotFound();

        List<BasketVm>? basketVm = GetBasket();
        BasketVm cartVm = basketVm.Find(x => x.Id == id);
        if (cartVm != null)
        {
            cartVm.Count++;
        }
        else
        {
            basketVm.Add(new BasketVm
            {
                Count = 1,
                Id = id
            });
        }
        Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketVm));
        return RedirectToAction("Index");
    }
    private List<BasketVm> GetBasket()
    {
        List<BasketVm> basketVms;
        if (Request.Cookies["basket"] != null)
        {
            basketVms = JsonConvert.DeserializeObject<List<BasketVm>>(Request.Cookies["basket"]);
        }
        else basketVms = new List<BasketVm>();
        return basketVms;
    }

    public IActionResult ChangePage(int page = 1, int pageSize = 1)
    {
        return ViewComponent("Product", new { page = page, pageSize = pageSize });
    }

    public async Task<IActionResult> Search(ProductSearchVm vm)
    {
        var products = _context.products.Include(x => x.ProductImgs)
          .Include(c => c.Category).AsQueryable();

        if (vm.CategoryId != null && vm.Name == null)
        {
            products.Where(x => x.CategoryId == vm.CategoryId);
        }
        else if (vm.Name != null && vm.CategoryId == null)
        {
            products.Where(x => x.Name.ToLower().StartsWith(vm.Name.ToLower()));
        }
        else
        {
            products.Where(x => x.CategoryId == vm.CategoryId && x.Name.ToLower().StartsWith(vm.Name.ToLower()));
        }
        return View(await products.ToListAsync());
    }
}

