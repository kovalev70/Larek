﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Model;
using OrderService.Repositories;

namespace OrderService.Controllers
{
	[Route("api/orders")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly OrderContext _context;

		public OrdersController(OrderContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Order>> PostOrders(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
		}

		[HttpPut("{id},{productId}")]
		public async Task<ActionResult<Order>> AddProductInOrder(Order order, int productId, int id)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			var products = await ProductRepository.GetProducts();
			var product = products.Where(product => product.Id == productId).FirstOrDefault();
			if (products.Where(product => product.Id == productId).Count() == 0)
			{
				return BadRequest();
			}	

			if (_context.OrdersProducts.Any(x => x.ProductId == productId && x.OrderId == id) == false) 
			{
				order.ProductsId.Add(productId);
				_context.OrdersProducts.Add(new OrdersProducts 
				{ 
					OrderId = id, 
					ProductId = productId, 
					Quantity = 1 
				});
			}
			else
			{
				var ordersProducts = _context.OrdersProducts.Where
					(x => x.ProductId == productId && x.OrderId == id).FirstOrDefault();
				ordersProducts.Quantity += 1;
			}
			_context.Orders.Where
					(x => x.Id == id).FirstOrDefault().TotalPrice += product.Price;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Orders.Any(e => e.Id == id)))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			return order;
		}

		[HttpGet("{id}/products")]
		public async Task<ActionResult<IEnumerable<OrdersProducts>>> GetProductsIdInOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}
			var orderProducts = from x in _context.OrdersProducts where x.OrderId == id select x;
			return await orderProducts.ToListAsync();
		}

		[HttpGet("{id}/{productID}/quantity")]
		public async Task<ActionResult<int>> GetProductQuantityInOrder(int id, int productId)
		{
			var orderProducts = await _context.OrdersProducts.FindAsync(id, productId);

			if (orderProducts == null)
			{
				return NotFound();
			}
			return orderProducts.Quantity;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrder(int id, Order order)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			_context.Entry(order).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Orders.Any(e => e.Id == id)))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Order>> DeleteOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return order;
		}
	}
}

