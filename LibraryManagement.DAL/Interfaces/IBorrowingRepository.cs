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

        Task<int> RecordNewBorrowingAsync(Borrowing borrowingEntity);

        Task<int> UpdateBorrowingAsync(Borrowing borrowingEntity);

        Task<Borrowing?> FindBorrowingByIDAsync(int borrowingID);


    }
}