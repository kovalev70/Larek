using DeliveryService.Data;
using DeliveryService.Model;
using DeliveryService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DeliveryService.Controllers
{
	[Route("api/deliveries")]
	[ApiController]
	public class DeliveryController : ControllerBase
	{
		private readonly DeliveryContext _context;

		public DeliveryController(DeliveryContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
		{
			_context.Deliveries.Add(delivery);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDelivery), new { id = delivery.Id }, delivery);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
		{
			return await _context.Deliveries.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Delivery>> GetDelivery(int id)
		{
			var product = await _context.Deliveries.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrderInDelivery(int id, int orderId)
		{
			var delivery = await _context.Deliveries.FindAsync(id);

			if (delivery == null)
			{
				return NotFound();
			}
			delivery.OrderId = orderId;
			_context.Entry(delivery).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Deliveries.Any(e => e.Id == id)))
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
		public async Task<ActionResult<Delivery>> DeleteDelivery(int id)
		{
			var delivery = await _context.Deliveries.FindAsync(id);
			if (delivery == null)
			{
				return NotFound();
			}

			_context.Deliveries.Remove(delivery);
			await _context.SaveChangesAsync();

			return delivery;
		}

		[HttpPut("result")]
		public async Task<IActionResult> DeliveryResult(int deliveryId, bool result)
		{
			var delivery = await _context.Deliveries.FindAsync(deliveryId);
			if (delivery == null)
			{
				return NotFound();
			}

			if (result)
			{
				var productOrders = OrderRepository.GetProductsIdFromOrder(delivery.OrderId).Result;
				foreach (var productOrder in productOrders)
				{
					var productId = productOrder.ProductId;
					await OrderRepository.PickUpProducts
						(productId,productOrder.Quantity);
				}
				delivery.Delivered = true;
				await _context.SaveChangesAsync();
				return Ok("Delivery is successful");
			}

			delivery.Delivered = false;

			await _context.SaveChangesAsync();

			return Ok("Cancellation of delivery");
		}
	}
}
