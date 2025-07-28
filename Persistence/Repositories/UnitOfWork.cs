using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Domain.Repositories;

namespace Supermarket.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
