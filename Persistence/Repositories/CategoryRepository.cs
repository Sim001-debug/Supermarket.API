using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Supermarket.API.Data;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Models;
using Supermarket.API.Resource;

namespace Supermarket.API.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger; 

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger) : base(context)
        {
            _logger = logger;
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
            try
            {
                var existingCategory = await _context.Categories.FindAsync(categoryId);

                if (existingCategory == null)
                {
                    _logger.LogWarning($"Category with ID {categoryId} not found for update.");
                    return null;
                }

                existingCategory.Name = updateCategory.Name;

                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();

                return existingCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category with ID {categoryId}", categoryId);
                return null;
            }
            
        }

        public Task<Category> FindByIdAsync(int id)
        {
            return _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        //public void Update(CategoryResource categoryResource)
        //{
        //    return; // This method is not implemented in the repository, it should be handled in the service layer.
        //}

        public async Task<Category> DeleteByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found for deletion.");
                return null;
            }

            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting category with ID {id}");
                return null;
            }
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

    }
}
