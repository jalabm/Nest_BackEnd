﻿using Nest_6._03.Models;

namespace Nest_6._03.ViewModels;

public class ProductVm
	{
	public Product product { get; set; }
	public List<Product> Products { get; set; }
    public List<Category> Categories { get; set; }
}

