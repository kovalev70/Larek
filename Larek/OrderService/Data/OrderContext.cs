using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OrderService.Model;
using System.Xml;

namespace OrderService.Data
{
	public class OrderContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrdersProducts> OrdersProducts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite("Data Source=order.db");
		}

		public OrderContext(DbContextOptions<OrderContext> options)
			: base(options)
		{
			Database.Migrate();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrdersProducts>().HasKey(e => new { e.OrderId, e.ProductId });
		}

	}
}
