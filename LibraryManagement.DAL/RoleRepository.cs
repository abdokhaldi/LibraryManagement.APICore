using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LibraryDbContext _context;
        public RoleRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public Task<IQueryable<Role>> GetQueryableRolesAsync()
        {
           
var            query = _context.Roles.AsQueryable();
                return Task.FromResult(query);
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
