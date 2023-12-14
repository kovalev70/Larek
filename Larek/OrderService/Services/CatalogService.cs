using OrderService.Model;
using System.Text.Json;
using OrderService.Interfaces;

namespace OrderService.Services
{
	public class CatalogService: ICatalogService
	{
		private readonly HttpClient _httpClient;

		public CatalogService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<Product?> GetProductAsync(
			int productId,
			CancellationToken cancellationToken = default)
		{
			var path = $"/api/products/{productId}";
			var response = await _httpClient.GetAsync(path, cancellationToken);

			response.EnsureSuccessStatusCode();

			var content = await response.Content
				.ReadAsStringAsync(cancellationToken);

			var result = JsonSerializer.Deserialize<Product>(
				content,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
	}
}
