using DeliveryService.Interfaces;
using DeliveryService.Model;
using System.Text.Json;

namespace DeliveryService.Services
{
	public class OrderService: IOrderService
	{
		private readonly HttpClient _httpClient;

		public OrderService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<OrdersProducts>?> GetProductsIdFromOrder(
			int orderId, 
			CancellationToken cancellationToken = default)
		{
			var path = $"/api/orders/{orderId}/products";

			var response = await _httpClient.GetAsync(path);

			response.EnsureSuccessStatusCode();

			var content = await response.Content
				.ReadAsStringAsync(cancellationToken);

			var result = JsonSerializer.Deserialize<List<OrdersProducts>>(
				content,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
