using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories
{
    public interface IFineRepository
    {
        Task AddNewFineAsync(Fine fineEntity);
    }
}
