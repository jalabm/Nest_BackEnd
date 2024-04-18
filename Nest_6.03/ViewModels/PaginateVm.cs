using System;
using Nest_6._03.Models;

namespace Nest_6._03.ViewModels
{
	public class PaginateVm
	{
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}

