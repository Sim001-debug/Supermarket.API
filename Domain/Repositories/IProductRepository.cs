using Supermarket.API.Models;

namespace Supermarket.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> ListAsync();
    }
}
