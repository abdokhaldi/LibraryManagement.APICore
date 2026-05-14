using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.TenantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public interface ITenantService
    {
        Task<OperationResult<TenantForDisplayDTO>> GetTenantAsync(Guid id);
        Task<OperationResult> UpdateTenantAsync(Guid id, TenantForUpdateDTO tenant);
    }
}
