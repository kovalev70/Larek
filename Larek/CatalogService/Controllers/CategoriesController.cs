using CatalogService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

namespace CatalogService.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly CatalogContext _context;

		public CategoriesController(CatalogContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _context.Categories.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);

			if (category == null)
			{
				return NotFound();
			}

			return category;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!(_context.Categories.Any(e => e.Id == id)))
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
		public async Task<ActionResult<Category>> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return category;
		}
	}
}