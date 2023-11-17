using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using OrderService.Data;
using OrderService.Interfaces;
using OrderService.Model;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
	public class ProductRepository
	{
		public static async Task<List<IProduct>> GetProducts()
		{
			using (var client = new HttpClient())
			{
				string apiUrl = "https://localhost:5052/api/products/";

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.ReadFromJsonAsync<List<IProduct>>();
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
