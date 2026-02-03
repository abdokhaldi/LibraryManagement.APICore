using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleForReadOnlyAsync(int roleID);
        Task<bool> IsRoleExistingAsync(string roleName);
        Task<List<Role>> GetRolesAsync();
        Task CreateRole(Role role);
    }
}
