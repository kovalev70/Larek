using DeliveryService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DeliveryService.Data
{
	public class DeliveryContext:DbContext
	{
		public DbSet<Delivery> Deliveries { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			Directory.CreateDirectory("..\\Db");
			options.UseSqlite($"Data Source=..\\Db\\deliveries.db");
		}
	}
}
