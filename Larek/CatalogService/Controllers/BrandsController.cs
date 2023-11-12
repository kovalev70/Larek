using CatalogService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;
namespace CatalogService.Controllers
{
	[Route("api/brands")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly CatalogContext _context;

		public BrandsController(CatalogContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
		{
			return await _context.Brands.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Brand>> PostBrands(Brand brand)
		{
			_context.Brands.Add(brand);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Brand>> GetBrand(int id)
		{
			var brand = await _context.Brands.FindAsync(id);

			if (brand == null)
			{
				return NotFound();
			}

			return brand;
		}
	}
}