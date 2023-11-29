using DeliveryService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DeliveryService.Repositories
{
	public static class OrderRepository
	{
		public static async Task<bool> PickUpProducts(int productId, int quantity)
		{
			using (var client = new HttpClient())
			{
				string apiUrl = $"https://localhost:5052/api/products/pickup?id={productId}&quantity={quantity}";
				HttpResponseMessage response = await client.PutAsync(apiUrl, null);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static async Task<List<OrdersProducts>> GetProductsIdFromOrder(int orderId)
		{
			using (var client = new HttpClient())
			{
				string apiUrl = $"https://localhost:5053/api/orders/{orderId}/products";

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					var responseData = (await response.Content.ReadFromJsonAsync<List<OrdersProducts>>()).ToList();

					return responseData;
				}
				else
				{
					return null;
				}

			}
		}
	}
}
