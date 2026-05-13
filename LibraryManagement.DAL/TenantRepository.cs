using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagement.DAL
{
    public class TenantRepository : ITenantRepository
    {
        private readonly LibraryDbContext _context;
        public TenantRepository(LibraryDbContext context) {
            _context = context;
        }

       public Task CreateTenantAsync(Tenant tenant)
        {
                        _context.Tenants.Add(tenant);
               return  Task.CompletedTask;
        }
        public async Task<Tenant?> GetTenantAsync(Expression<Func<Tenant, bool>> predicate, bool tracking = false)
        {
            var query =  _context.Tenants.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> IsTenantIdentitierExistAsync(string identifier)
              => await _context.Tenants.AnyAsync(t => t.Identifier == identifier);
    
      
    }
}
