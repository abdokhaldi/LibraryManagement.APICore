using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.FineDTO;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IFineService
    {
        Task<OperationResult> WaiveAsync(int id, string waiveReason);
        Task<OperationResult> PayAsync(int id);
        Task<PagedList<FineDtoForDisplay>> GetFinesAsync(FineParameters parameters);
    }
}
