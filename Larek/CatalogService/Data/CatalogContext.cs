using CatalogService.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CatalogService.Data
{ 
	public class CatalogContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Brand> Brands { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			Directory.CreateDirectory("..\\Db");
			options.UseSqlite($"Data Source=..\\Db\\catalog.db");
		}

		public CatalogContext(DbContextOptions<CatalogContext> options)
			: base(options)
		{
			Database.Migrate();
		}
	}
}
