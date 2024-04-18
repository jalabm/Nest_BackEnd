using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Models;

namespace Nest_6._03.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Product> products {get;set;}
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductImg> productimgs { get; set; }
		public DbSet<Vendor> vendors { get; set; }
		public DbSet<NavBar> navBars { get; set; }
		public DbSet<SubNavBar> subNavBars { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.SoftDelete);
            base.OnModelCreating(modelBuilder);
        }
    }
}

