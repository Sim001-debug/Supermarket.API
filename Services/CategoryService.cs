using Supermarket.API.Domain.Repositories;
using Supermarket.API.Models;

namespace Supermarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return _categoryRepository.ListAsync();
        }

        public Task CreateCategoryAsync(Category category)
        {
            // Implementation for creating a category
            // Assuming the repository has a method to add a category
            return _categoryRepository.AddAsync(category);
        }

        public Task<Category> UpdateCategoryService(int categoryId, Category updateCategory)
        {
            return _categoryRepository.UpdateAsync(categoryId, updateCategory);
        }
    }
}
