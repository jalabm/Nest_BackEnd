﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Nest_6._03.Models;

public class Product
	{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public double? Rating { get; set; }
    public decimal SellPrice { get; set; } = default!;
    public decimal? DiscountPrice { get; set; }
    public bool SoftDelete { get; set; }
    [NotMapped]
    public List<IFormFile>? Files { get; set; }
    [NotMapped]
    public IFormFile MainFile { get; set; }
    [NotMapped]
    public IFormFile HoverFile { get; set; }
    public List<ProductImg> ProductImgs { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;
    public ICollection<ProductSize> ProductSizes { get; set; }
    public ICollection<ProductWeight> ProductWeights { get; set; }
    public Product()
    {
        ProductSizes = new HashSet<ProductSize>();
        ProductWeights = new HashSet<ProductWeight>();
        ProductImgs = new();
    }   
}


