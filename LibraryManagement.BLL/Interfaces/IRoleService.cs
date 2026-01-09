using LibraryManagement.DTO.RoleDTOs;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<OperationResult<int>> CreateRoleAsync(RoleForCreationDTO roleDTO);
        Task<OperationResult<RoleForDisplayDTO>> GetRoleAsync(int id);
    }
}
