using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.Domain.Repositories
{
    public interface IFineRepository
    {
        Task AddNewFineAsync(Fine fineEntity);
        Task<Fine?> GetFineByIdAsync(int id);
        Task<PagedList<Fine>> GetFinesAsync(FineParameters parameters);
        
    }
}
