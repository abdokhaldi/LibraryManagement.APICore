using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DTO.OperationResult;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<OperationResult<int>> CreateRoleAsync(RoleForCreationDTO roleDTO);
        Task<OperationResult<RoleForDisplayDTO>> GetRoleAsync(int id);
    }
}
