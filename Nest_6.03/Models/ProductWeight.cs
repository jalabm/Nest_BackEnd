using System;
namespace Nest_6._03.Models
{
	public class ProductWeight
	{
        public int Id { get; set; }
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public Weight Weight { get; set; } = null!;
        public int WeightId { get; set; }
        public int Count { get; set; }
    }
}

