using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IQueryable<Borrowing>> GetQueryableBorrowingsAsync();

        Task<bool> IsBookCurrentlyUnavailableAsync(int bookID, int personID);

        Task RecordNewBorrowingAsync(Borrowing borrowingEntity);

        Task UpdateBorrowingAsync(Borrowing borrowingEntity);

        Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID);
        Task<Borrowing?> FindBorrowingForReadOnlyAsync(int borrowingID);


    }
}