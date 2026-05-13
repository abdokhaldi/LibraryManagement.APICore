

using LibraryManagement.Domain.Entities.Tenants;
using System.Linq.Expressions;

namespace LibraryManagement.Domain.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetTenantAsync(Expression<Func<Tenant, bool>> predicate, bool tracking = false);
        Task CreateTenantAsync(Tenant tenant);
        Task<bool> IsTenantIdentitierExistAsync(string identifier);
  }
}
