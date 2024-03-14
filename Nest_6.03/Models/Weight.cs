using System;
namespace Nest_6._03.Models
{
	public class Weight
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductWeight> ProductWeights { get; set; }
        public Weight()
        {
            ProductWeights = new HashSet<ProductWeight>();
        }
    }
}

