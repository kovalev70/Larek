using DeliveryService.Model;

namespace DeliveryService.Interfaces
{
	public interface IOrderService
	{
		Task<List<OrdersProducts>> GetProductsIdFromOrder(
			int orderId,
			CancellationToken cancellationToken = default);
	}
}
