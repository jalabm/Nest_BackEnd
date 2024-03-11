using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest_6._03.Controllers
{
    public class VendorController : Controller
    {
        public readonly AppDbContext _context;
        public VendorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vendors = await _context.vendors.
                Include(x=>x.Products).
                OrderByDescending(x=>x.Id).Take(16).ToListAsync();
            return View(vendors);
        }
    }
}

