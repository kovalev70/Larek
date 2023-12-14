namespace DeliveryService.Model
{
	public class Delivery
	{
		public int Id { get; set; }
		public bool Delivered { get; set; } = false;
		public int OrderId { get; set; }
	}
}
