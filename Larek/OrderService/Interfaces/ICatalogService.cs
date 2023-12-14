using OrderService.Model;

namespace OrderService.Interfaces
{
	public interface ICatalogService
	{
			Task<Product?> GetProductAsync( 
				int productId, 
				CancellationToken cancellationToken = default);
	}
}
