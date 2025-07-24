using Supermarket.API.Models;

namespace Supermarket.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> UpdateAsync(int categoryId, Category updateCategory);
    }
}
