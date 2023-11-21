using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using OrderService.Data;
using OrderService.Model;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Abstracts;

namespace OrderService.Repositories
{
	public class ProductRepository
	{
		public static async Task<List<Product>> GetProducts()
		{
			using (var client = new HttpClient())
			{
				string apiUrl = "https://localhost:5052/api/products/";

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.ReadFromJsonAsync<List<Product>>();
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
