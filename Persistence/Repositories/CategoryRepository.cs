using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Models;

namespace Supermarket.API.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(int categoryId, Category updateCategory)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryId);

            if (existingCategory == null)
                return null;

            existingCategory.Name = updateCategory.Name;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}
