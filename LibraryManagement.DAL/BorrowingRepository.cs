
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using LibraryManagement.DAL.Interfaces;

namespace LibraryManagement.DAL
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowingRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBookCurrentlyUnavailableAsync(int bookID,int personID)
        {
           
                bool IsBookCurrentlyUnavailable = await _context.Borrowings.AnyAsync(
                                             b => b.BookID == bookID
                                             && b.PersonID == personID
                                             && b.ReturnDate == null
                                             && b.IsCanceled == false
                                            );
                return IsBookCurrentlyUnavailable;
            }
            
        public Task RecordNewBorrowingAsync(Borrowing borrowingEntity)
        {
            _context.Borrowings.Add(borrowingEntity);
            return Task.CompletedTask;
        }
            
        
        public  Task UpdateBorrowingAsync(Borrowing borrowingEntity)
        {
          
                _context.Borrowings.Update(borrowingEntity);
                return Task.CompletedTask;
            
        }
        

        public  async Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID)
        {
           
                var borrowing = await _context.Borrowings
                                        .Include(b => b.Person)
                                        .Include(b => b.Book)
                                        .FirstOrDefaultAsync(b=>b.BorrowingID == borrowingID);
                return borrowing;
            }

        public async Task<Borrowing?> FindBorrowingForReadOnlyAsync(int borrowingID)
        {

            var borrowing = await _context.Borrowings.AsNoTracking()
                                    .Include(b => b.Person)
                                    .Include(b => b.Book)
                                    .FirstOrDefaultAsync(b => b.BorrowingID == borrowingID);
            return borrowing;
        }



        public  Task<IQueryable<Borrowing>> GetQueryableBorrowingsAsync()
        {
            var query =  _context.Borrowings.AsQueryable();
            return Task.FromResult(query);
        }
         


    }
}
