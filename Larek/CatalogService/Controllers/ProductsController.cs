using CatalogService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

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
			var product = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

			if (!_context.Products.Where(p => p.CategoryId == categoryId).Any())
			{
				return NotFound();
			}

			return product;
		}

		[HttpGet("BrandId/{brandId}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(int brandId)
		{
			var product = await _context.Products.Where(p => p.BrandId == brandId).ToListAsync();

			if (!_context.Products.Where(p => p.BrandId == brandId).Any())
			{
				return NotFound();
			}

			return product;
		}

		[HttpGet("NamePart/{namePart}")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByNamePart(string namePart)
		{
			var product = await _context.Products.Where(p => p.Name.Contains(namePart)).ToListAsync();

			if (!_context.Products.Where(p => p.Name.Contains(namePart)).Any())
			{
				return NotFound();
			}

			return product;
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
	}
}