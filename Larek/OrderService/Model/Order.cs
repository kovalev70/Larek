namespace OrderService.Model
{
	public class Order
	{
		public int Id { get; set; }
		public decimal TotalPrice { get; set; } = 0;
		public int CustomerId { get; set; }
		public bool NeedForDelivery { get; set; }
		public List<int> ProductsId { get; } = new();
		public List<OrdersProducts> OrdersProducts { get; } = new();
	}
}
