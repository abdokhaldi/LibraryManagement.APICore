using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<PagedList<Borrowing>> GetBorrowingsAsync(BorrowingParameters parameters);

        Task<bool> IsBookCurrentlyUnavailableAsync(int bookID, int personID);

        Task RecordNewBorrowingAsync(Borrowing borrowingEntity);
        Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID);
        Task<Borrowing?> GetBorrowingForReadOnlyAsync(int borrowingID);
       

    }
}