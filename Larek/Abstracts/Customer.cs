namespace Abstracts
{
	public class Customer
	{
		public int Id { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerPhoneNumber { get; set; }
		public string? DeliveryAddress { get; set; }

		public ICollection<Order> Orders { get; } = new List<Order>();
	}
}
