using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace LibraryManagement.DAL
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LibraryDbContext _context;
        public RoleRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRolesAsync()
        {

           return await _context.Roles.AsNoTracking().ToListAsync();
        }
           
        public async Task<bool> IsRoleExistingAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.RoleName == roleName);
        }

        public async Task<Role?> GetRoleForReadOnlyAsync(int roleID)
        {
            
                var role = await _context.Roles
                .AsNoTracking()
                .Where(r=>r.RoleID ==roleID)
                .FirstOrDefaultAsync();
                return role;
            }
        public async Task<Role?> GetRoleForUpdateAsync(int roleID)
        {

            var role = await _context.Roles.FindAsync(roleID);
            return role;
        }
        public Task CreateRole(Role role)
        {
            _context.Roles.Add(role);
            return Task.CompletedTask;
        }
    }
}
