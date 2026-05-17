using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.TenantDTOs;

namespace LibraryManagement.BLL.Interfaces
{
    public interface ITenantService
    {
        Task<OperationResult<TenantForDisplayDTO>> GetTenantAsync(Guid id);
        Task<OperationResult> UpdateTenantAsync(Guid id, TenantForUpdateDTO tenant);
    }
}
