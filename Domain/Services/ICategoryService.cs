using Supermarket.API.Models;
using Supermarket.API.Resource;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> UpdateCategoryService(int categoryId, Category updateCategory);
    Task<Category> FindByIdAsync(int id);
    void Update(CategoryResource categoryResource);
    Task<Category> DeleteCategoryAsync(int id);
}
