using Supermarket.API.Models;
using Supermarket.API.Resource;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> UpdateCategoryService(int categoryId, Category updateCategory);
    Task<Category> FindByIdAsync(int id);
    Task<Category> DeleteCategoryAsync(int id);
    //Task<CategoryResponse> GetCategoryByIdAsync(int id);
}
