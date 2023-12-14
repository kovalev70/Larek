namespace DeliveryService.Interfaces
{
	public interface ICatalogService
	{
		Task<bool> PickUpProducts(
			int productId, 
			int quantity,
			CancellationToken cancellationToken = default);
	}
}
