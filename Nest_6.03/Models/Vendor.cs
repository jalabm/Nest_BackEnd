using System;
namespace Nest_6._03.Models
{
	public class Vendor
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
        public double? Rating { get; set; }
		public string Address { get; set; } = null!;
		public int Number { get; set; }
		public bool SoftDelete { get; set; }
		public List<Product>? Products { get; set; }
	}
}

