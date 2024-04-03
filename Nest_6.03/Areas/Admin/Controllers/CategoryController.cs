using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.Extensions;
using Nest_6._03.Models;

namespace Nest_6._03.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CategoryController(AppDbContext context,IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async  Task<IActionResult> Index()
    {
        var categories = await _context.categories.Include(x => x.Products).Where(x => !x.SoftDelete).ToListAsync();
        return View(categories);
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        
        if (!ModelState.IsValid)
            return View(category);
        if (!category.File.CheckFileType("image"))
        {
            ModelState.AddModelError("", "Invalid File");
            return View(category);
        }
        if (!category.File.CheckFileSize(2))
        {   
            ModelState.AddModelError("", "Invalid File Size");
            return View(category);
        }
        var isExist =await _context.categories.AnyAsync(x => x.Name.ToLower() == category.Name.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Name", "category name is already exist");
            return View();

        }
        string uniqueFileName = await category.File.SaveFileAsync(_env.WebRootPath, "assets","imgs", "categoryIcons");

        Category newCategory = new Category
        {
            Name = category.Name,
            Icon = uniqueFileName,
        };

        await _context.categories.AddAsync(newCategory);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id) return BadRequest();
        Category? existsCategory = await _context.categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existsCategory == null) return NotFound();
        if (category.File != null)
        {
            if (!category.File.CheckFileSize(2))
            {
                ModelState.AddModelError("File", "File size can not be more than 2mb");
                return View(category);
            }
            if (!category.File.CheckFileType("image"))
            {
                ModelState.AddModelError("File", "File type is invalid");   
                return View(category);
            }

            var path = Path.Combine(_env.WebRootPath,  "assets", "imgs","categoryIcons", existsCategory.Icon);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var uniqueFileName = await category.File.
                SaveFileAsync(_env.WebRootPath, "assets", "imgs", "categoryIcons");

            existsCategory.Icon = uniqueFileName;
            existsCategory.Name = category.Name;
            _context.Update(existsCategory);
        }
        else
        {
            category.Icon = existsCategory.Icon;
            _context.categories.Update(category);

        }
        await _context.SaveChangesAsync();
        if (category.Name == null)
        {
            return RedirectToAction("Edit", new { id = id });
        }
        return RedirectToAction("Index");

    }
    public async Task<IActionResult> Delete(int id)
    {
        Category? category = await _context.categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
        {
            return NotFound();
        }
        category.SoftDelete = true;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    
}

