using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IQueryable<Borrowing>> GetBorrowingsAsync();

        Task<bool> IsBookCurrentlyUnavailableAsync(int bookID, int personID);

        Task RecordNewBorrowingAsync(Borrowing borrowingEntity);
        Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID);
        Task<Borrowing?> GetBorrowingForReadOnlyAsync(int borrowingID);
       

    }
}