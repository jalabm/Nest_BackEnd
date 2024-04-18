using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;

namespace Nest_6._03.ViewComponents
{
	public class SearchViewComponent:ViewComponent
	{
        private readonly AppDbContext _context;

        public SearchViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductSearchVm vm = new ProductSearchVm()
            {
                Categories = await _context.categories.ToListAsync()
            };
            return View(vm);
        }
    }
}

