namespace CatalogService.Model
{
	public class Category
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		public ICollection<Product> Product { get; } = new List<Product>();
	}
}
