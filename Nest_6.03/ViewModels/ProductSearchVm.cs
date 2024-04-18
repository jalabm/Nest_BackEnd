using System;
using Nest_6._03.Models;

namespace Nest_6._03.ViewModels
{
	public class ProductSearchVm
	{
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public List<Category> Categories { get; set; }
    }
}

