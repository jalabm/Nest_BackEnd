using System;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Models;

namespace Nest_6._03.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Product> products {get;set;}
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductImg> productimgs { get; set; }
		public DbSet<Vendor> vendors { get; set; }
	}
}

