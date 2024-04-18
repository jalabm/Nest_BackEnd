using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.Dtos;
using Nest_6._03.Extensions;
using Nest_6._03.Models;

namespace Nest_6._03.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{

    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }



    public async Task<IActionResult> Create()
    {

        var categories = await _context.categories.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Categories = categories;
        var Vendors = await _context.vendors.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Vendors = Vendors;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var categories = await _context.categories.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Categories = categories;
        var Vendors = await _context.vendors.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Vendors = Vendors;
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        foreach (var file in dto.Files)
        {
            if (!file.CheckFileType("image"))
            {
                ModelState.AddModelError("Files", "Invalid FIle type");
                return View(dto);
            }
            if (!file.CheckFileSize(2))
            {
                ModelState.AddModelError("Files", "Invalid FIle Size");
                return View(dto);
            }

        }
        if (!dto.MainFile.CheckFileSize(2))
        {
            ModelState.AddModelError("MainFile", "Invalid FIle Size");
            return View(dto);
        }
        if (!dto.MainFile.CheckFileType("image"))
        {
            ModelState.AddModelError("MainFile", "Invalid FIle Type");
            return View(dto);
        }
        if (!dto.HoverFile.CheckFileSize(2))
        {
            ModelState.AddModelError("HoverFile", "Invalid FIle Size");
            return View(dto);
        }
        if (!dto.HoverFile.CheckFileType("image"))
        {
            ModelState.AddModelError("HoverFile", "Invalid FIle Type");
            return View(dto);
        }

        var isExistCategory = await _context.categories.AnyAsync(x => x.Id == dto.CategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("categoryId", "Invalid Category ");
            return View(dto);
        }
        var isExistVendor = await _context.vendors.AnyAsync(x => x.Id == dto.VendorId);
        if (!isExistVendor)
        {
            ModelState.AddModelError("VendorId", "Invalid Vendor ");
            return View(dto);
        }
        Product product = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Rating = dto.Rating,
            SellPrice = dto.SellPrice,
            DiscountPrice = dto.DiscountPrice,
            VendorId = dto.VendorId,
            CategoryId = dto.CategoryId

        };



        var mainFileName = await dto.MainFile.SaveFileAsync(_env.WebRootPath, "assets", "imgs", "productImg");
        var hoverFileName = await dto.HoverFile.SaveFileAsync(_env.WebRootPath, "assets", "imgs", "productImg");

        ProductImg mainFile = new() { IsMain = true, IsHower = false, Url = mainFileName, Product = product };

        ProductImg hoverFile = new() { IsMain = false, IsHower = true, Url = hoverFileName, Product = product };
        foreach (var file in dto.Files)
        {
            var fileName = await file.SaveFileAsync(_env.WebRootPath, "assets", "imgs", "productImg");
            ProductImg img = new() { IsHower = false, IsMain = false, Url = fileName, Product = product };
            product.ProductImgs.Add(img);
        }

        product.ProductImgs.Add(mainFile);
        product.ProductImgs.Add(hoverFile);

        await _context.products.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("index");


    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.products
                               .Include(x => x.Category)
                               .Include(x => x.ProductImgs)
                               .Include(x => x.Vendor)
                               .Where(x => !x.SoftDelete)
                               .ToListAsync();
        return View(products);

    }

    public ProductImg CreateProduct(string url, bool isHover, bool isMain, Product product)
    {
        return new ProductImg
        {
            Url = url,
            IsHower = isHover,
            IsMain = isMain,
            Product = product
        };
    }


    public async Task<IActionResult> Update(int id)
    {

        var product = await _context.products.Include(x=>x.ProductImgs).FirstOrDefaultAsync(x => x.Id == id);
        if (product is null) return NotFound();

        ProductUpdateDto dto = new()
        {
            Name = product.Name,
            Description = product.Description,
            Rating = product.Rating,
            SellPrice = product.SellPrice,
            DiscountPrice = product.DiscountPrice,
            CategoryId = product.CategoryId,
            VendorId = product.VendorId,
            MainFilePath = product.ProductImgs.FirstOrDefault(x => x.IsMain)?.Url,
            HoverFilePath = product.ProductImgs.FirstOrDefault(x => x.IsHower)?.Url,
            FilePaths = product.ProductImgs.Where(x => !x.IsHower && !x.IsMain).Select(x => x.Url).ToList()

        };
        var categories = await _context.categories.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Categories = categories;
        var Vendors = await _context.vendors.Where(x => !x.SoftDelete).ToListAsync();
        ViewBag.Vendors = Vendors;
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id || id == null || id < 1) return BadRequest();

        var existProduct = await _context.products.FindAsync(id);


        if (product.Files != null)
        {
            foreach (var file in product.Files)
            {

                if (!file.CheckFileSize(2))
                {
                    ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                    return View(product);
                }


                if (!file.CheckFileType("image"))
                {
                    ModelState.AddModelError("Files", "Files must be image type!");
                    return View(product);
                }
                var filename = await file.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
                var additionalProductImgs = CreateProduct(filename, false, false, product);
                existProduct.ProductImgs.Add(additionalProductImgs);
            }
        }
        if (product.MainFile != null)
        {
            if (!product.MainFile.CheckFileSize(2))
            {
                ModelState.AddModelError("MainFile", "Files cannot be more than 2mb");
                return View(product);
            }


            if (!product.MainFile.CheckFileType("image"))
            {
                ModelState.AddModelError("MainFile", "Files must be image type!");
                return View(product);
            }

            product.MainFile.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existProduct.ProductImgs.FirstOrDefault(x => x.IsMain).Url);
            var mainFileName = await product.MainFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
            var mainProductImage = CreateProduct(mainFileName, false, false, product);
            existProduct.ProductImgs.Add(mainProductImage);

        }
        if (product.HoverFile != null)
        {
            if (!product.HoverFile.CheckFileSize(2))
            {
                ModelState.AddModelError("HoverFile", "Files cannot be more than 2mb");
                return View(product);
            }


            if (!product.HoverFile.CheckFileType("image"))
            {
                ModelState.AddModelError("HoverFile", "Files must be image type!");
                return View(product);
            }

            product.HoverFile.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existProduct.ProductImgs.FirstOrDefault(x => x.IsHower).Url);
            var hoverFileName = await product.HoverFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
            var hoverProductImageCreate = CreateProduct(hoverFileName, true, false, product);
            existProduct.ProductImgs.Add(hoverProductImageCreate);
        }

        existProduct.Name = product.Name;
        existProduct.Description = product.Description;
        existProduct.SellPrice = product.SellPrice;
        existProduct.Rating = product.Rating;
        existProduct.DiscountPrice = product.DiscountPrice;
        existProduct.CategoryId = product.CategoryId;


        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Detail(int? id)
    {

        if (id == null || id <= 0) return BadRequest();

        var product = await _context.products
                               .Include(x => x.Category)
                               .Include(x => x.ProductImgs)
                               .Include(x => x.Vendor)
                               .Include(x => x.ProductSizes)
                               .ThenInclude(x => x.Size)
                               .Where(x => !x.SoftDelete)
                               .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);

    }

    [HttpPost]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var existsImage = await _context.productimgs.FindAsync(id);
        var product = await _context.products.Include(x => x.Category)
                                              .Include(x => x.ProductImgs)
                                              .FirstOrDefaultAsync(x => x.Id == existsImage.ProductId);
        existsImage.File.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existsImage.Url);
        _context.Remove(existsImage);
        await _context.SaveChangesAsync();
        return PartialView("_ProductImagePartial", product.ProductImgs);
    }
}

