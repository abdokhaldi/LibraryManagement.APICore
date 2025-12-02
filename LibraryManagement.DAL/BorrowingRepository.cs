
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
            try
            {
                bool IsBookCurrentlyUnavailable = await _context.Borrowings.AnyAsync(
                                             b => b.BookID == bookID
                                             && b.PersonID == personID
                                             && b.ReturnDate == null
                                             && b.IsCanceled == false
                                            );
                return IsBookCurrentlyUnavailable;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> RecordNewBorrowingAsync(Borrowing borrowingEntity)
        {
            try
            {
                var bookToBorrow = await _context.Books.FindAsync(borrowingEntity.BookID);
                
                if (bookToBorrow == null || bookToBorrow.IsActive == false || bookToBorrow.Quantity<=0)
                {
                    throw new InvalidOperationException("Book is unavailable or not found.");
                }
                
                bookToBorrow.Quantity--;

               _context.Borrowings.Add(borrowingEntity);
                await _context.SaveChangesAsync();
                return borrowingEntity.BorrowingID;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task<int> UpdateBorrowingAsync(Borrowing borrowingEntity)
        {
          
                _context.Borrowings.Update(borrowingEntity);
                return await _context.SaveChangesAsync();
            
        }
        

        public  async Task<Borrowing?> FindBorrowingByIDAsync(int borrowingID)
        {
            try
            {
                var borrowing = await _context.Borrowings.Include(b => b.Person)
                                        .Include(b => b.Book)
                                        .FirstOrDefaultAsync(b=>b.BorrowingID == borrowingID);
                return borrowing;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
           
        public  Task<IQueryable<Borrowing>> GetQueryableBorrowingsAsync()
        {
            var query =  _context.Borrowings.AsQueryable();
            return Task.FromResult(query);
        }
         


    }
}
