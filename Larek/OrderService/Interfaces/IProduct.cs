namespace OrderService.Interfaces
{
    public class IProduct
    {
		public int Id { get; set; }
		public string? Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
	}
}
