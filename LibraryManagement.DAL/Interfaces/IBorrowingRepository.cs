using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IQueryable<Borrowing>> GetQueryableBorrowingsAsync();

        Task<bool> IsBookCurrentlyUnavailableAsync(int bookID, int personID);

        Task RecordNewBorrowingAsync(Borrowing borrowingEntity);
        Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID);
        Task<Borrowing?> GetBorrowingForReadOnlyAsync(int borrowingID);
       

    }
}