using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> ListAsync();
    }
}
