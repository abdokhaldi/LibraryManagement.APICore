using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO;
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
    public class RoleRepository
    {
        private readonly LibraryDbContext _context;
        public RoleRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            try
            {
var            rolesList = await _context.Roles.ToListAsync();
                return rolesList;
            }
            catch (DbException ex)
            {
                throw;
            }
           catch( Exception ex)
            {
                throw;
            }



        }
        public async Task<Role?> GetRoleByIDAsync(int roleID)
        {
            try
            {
                var role = await _context.Roles.FindAsync(roleID);
                return role;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
