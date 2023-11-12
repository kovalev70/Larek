namespace CatalogService.Model
{
	public class Brand
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		public ICollection<Product> Product { get;} = new List<Product>();
	}
}
