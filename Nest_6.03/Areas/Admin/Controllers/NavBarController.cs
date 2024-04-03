using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest_6._03.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NavBarController : Controller
    {
        private readonly AppDbContext _context;
        public NavBarController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var model = await _context.navBars.ToListAsync();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( NavBar navBar)
        {
            if (!ModelState.IsValid) 
            {
                return View(navBar);
            }
            if (navBar.Order <=0) return BadRequest();

            NavBar newNavBar = new NavBar
            {
                Name = navBar.Name,
                Order = navBar.Order
            };


            await _context.navBars.AddAsync(newNavBar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

