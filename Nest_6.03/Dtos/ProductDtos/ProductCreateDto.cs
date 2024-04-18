using System.ComponentModel.DataAnnotations;

namespace Nest_6._03.Dtos;

public class ProductCreateDto
	{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    [Range(0,5)]
    public double? Rating { get; set; }
    public decimal SellPrice { get; set; } = default!;
    public decimal? DiscountPrice { get; set; }
    public IFormFile MainFile { get; set; } = null!;
    public IFormFile HoverFile { get; set; } = null!;
    public List<IFormFile> Files { get; set; } = null!;
    public int CategoryId { get; set; }
    public int VendorId { get; set; }
    
}

