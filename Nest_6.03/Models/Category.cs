using System;
namespace Nest_6._03.Models
{
	public class Category
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public List<Product>? Products { get; set; }
    }
}

