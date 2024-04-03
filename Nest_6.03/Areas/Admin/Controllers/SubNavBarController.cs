using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest_6._03.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubNavBarController : Controller
    {
        private readonly AppDbContext _context;

        public SubNavBarController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.subNavBars.ToListAsync();

            return View(model);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create()
        //{
           
        //    return View();
        //}
    }
}

