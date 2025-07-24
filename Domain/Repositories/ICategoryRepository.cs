using Supermarket.API.Models;
using Supermarket.API.Resource;

namespace Supermarket.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        Task<IEnumerable<Category>> ListAsync();
        void Update(CategoryResource categoryResource);
        Task<Category> UpdateAsync(int categoryId, Category updateCategory);
    }
}
