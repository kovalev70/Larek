namespace DeliveryService.Model
{
	public class Delivery
	{
		public int Id { get; set; }
		public bool Delivered { get; set; }
		public int OrderId { get; set; }
	}
}
