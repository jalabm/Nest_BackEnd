using System.ComponentModel.DataAnnotations;

namespace Nest_6._03.Dtos;

public class ProductUpdateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    [Range(0, 5)]
    public double? Rating { get; set; }
    public decimal SellPrice { get; set; } = default!;
    public decimal? DiscountPrice { get; set; }
    public IFormFile? MainFile { get; set; }
    public string? MainFilePath { get; set; }
    public IFormFile? HoverFile { get; set; } 
    public string? HoverFilePath { get; set; }
    public List<IFormFile> Files { get; set; } = new();
    public List<string> FilePaths { get; set; } = new();
    public int CategoryId { get; set; }
    public int VendorId { get; set; }

}

