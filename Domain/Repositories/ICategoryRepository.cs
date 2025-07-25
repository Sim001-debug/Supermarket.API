using Supermarket.API.Models;
using Supermarket.API.Resource;

namespace Supermarket.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        Task<IEnumerable<Category>> ListAsync();
       // void Update(CategoryResource categoryResource);
        Task<Category> UpdateAsync(int categoryId, Category updateCategory);
       //might need to remove the DeleteByIdAsync method
        Task<Category> DeleteByIdAsync(int id);

        void Remove(Category category);
    }
}
