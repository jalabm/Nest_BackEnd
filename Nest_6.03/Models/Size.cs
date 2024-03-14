using System;
namespace Nest_6._03.Models
{
	public class Size
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public Size()
        {
            ProductSizes = new HashSet<ProductSize>();
        }
    }
}


