using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<int> CreateRoleAsync(RoleForCreationDTO roleDTO);
        Task<RoleForDisplayDTO?> GetRoleAsync(int id);
    }
}
