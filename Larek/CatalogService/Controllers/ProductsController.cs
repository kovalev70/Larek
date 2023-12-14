using CatalogService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace CatalogService.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly CatalogContext _context;

		public ProductsController(CatalogContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		[HttpGet("CategoryId/{categoryId}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int categoryId)
		{
			var products = await _context.Products.Where
				(p => p.CategoryId == categoryId).ToListAsync();

			if (!products.Any())
			{
				return NotFound();
			}

			return products;
		}

		[HttpGet("BrandId/{brandId}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(int brandId)
		{
			var products = await _context.Products.Where
				(p => p.BrandId == brandId).ToListAsync();

			if (!products.Any())
			{
				return NotFound();
			}

			return products;
		}

		[HttpGet("NamePart/{namePart}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByNamePart(string namePart)
		{
			namePart = namePart.Trim();
			var allProducts = await _context.Products.ToListAsync();
			var products = allProducts.Where(p => p.Name.ToLower().Contains(namePart.ToLower())).ToList();

			if (!products.Any())
			{
				return NotFound();
			}

			return products;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Products.Any(e => e.Id == id)))
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
		public async Task<ActionResult<Product>> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return product;
		}
		[HttpPut("pickup")]
		public async Task<IActionResult> PickUpProducts(int id, int quantity)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}
			product.Quantity -= quantity;

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Products.Any(e => e.Id == id)))
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

		[HttpPut("add")]
		public async Task<IActionResult> AddProducts(int id, int quantity)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}
			product.Quantity += quantity;

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Products.Any(e => e.Id == id)))
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
	}
}