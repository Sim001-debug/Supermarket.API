using Supermarket.API.Models;
using Supermarket.API.Resource;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> UpdateCategoryService(int categoryId, Category updateCategory);
    Task FindByIdAsync(Category category);
}
