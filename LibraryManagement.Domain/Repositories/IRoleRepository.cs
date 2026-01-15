using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleForReadOnlyAsync(int roleID);

        Task<IQueryable<Role>> GetQueryableRolesAsync();
        Task CreateRole(Role role);
    }
}
