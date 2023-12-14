using DeliveryService.Interfaces;
using System.Text.Json;

namespace DeliveryService.Services
{
	public class CatalogService: ICatalogService
	{
		private readonly HttpClient _httpClient;

		public CatalogService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> PickUpProducts(
			int productId, 
			int quantity, 
			CancellationToken cancellationToken = default)
		{
			var path = $"/api/products/pickup?id={productId}&quantity={quantity}";
			var response = await _httpClient.PutAsync(path, null, cancellationToken);

			response.EnsureSuccessStatusCode();

			return true;
		}
	}
}
