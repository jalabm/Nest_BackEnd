using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest_6._03.Models
{
	public class ProductImg
	{
		public int Id { get; set; }
        public string Url { get; set; } = null!;
        [NotMapped]
        public IFormFile File { get; set; } = null!;
        public  bool  IsMain{ get; set; }
        public bool IsHower { get; set; }
        public int ProdictId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

