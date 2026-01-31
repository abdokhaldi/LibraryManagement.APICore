
using LibraryManagement.Domain.Entities;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.DAL.Base;

namespace LibraryManagement.DAL
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowingRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsBookCurrentlyUnavailableAsync(int bookID,int memberID)
        {
           
                bool IsBookCurrentlyUnavailable = await _context.Borrowings.AnyAsync(
                                             b => b.BookID == bookID
                                             && b.MemberID == memberID
                                             && b.ReturnDate == null
                                             && b.IsCanceled == true
                                            );
                return IsBookCurrentlyUnavailable;
            }
            
        public Task RecordNewBorrowingAsync(Borrowing borrowingEntity)
        {
            _context.Borrowings.Add(borrowingEntity);
            return Task.CompletedTask;
        }
            
        public async Task<Borrowing?> GetBorrowingForUpdateAsync(int borrowingID)
        {
           
                var borrowing = await _context.Borrowings
                                      .FindAsync(borrowingID);
                return borrowing;
            }

        public async Task<Borrowing?> GetBorrowingForReadOnlyAsync(int borrowingID)
        {

            var borrowing = await _context.Borrowings.AsNoTracking()
                                    .Include(b => b.Book)
                                    .Include(m => m.Member)
                                    .ThenInclude(m=>m.Person)
                                    .FirstOrDefaultAsync(b => b.BorrowingID == borrowingID);
            return borrowing;
        }

        public  Task<PagedList<Borrowing>> GetBorrowingsAsync(BorrowingParameters parameters)
        {
            var query =  _context.Borrowings
                                 .Include(b => b.MemberID)
                                 .Include(c => c.BookCopyID)
                                 .AsQueryable();

            query = query.Where(b => b.IsCanceled == parameters.IsCanceled);
            

            if (parameters.MemberID.HasValue && parameters.MemberID != 0)
            {
                query = query.Where(b=> b.MemberID == parameters.MemberID);
            }

            if (parameters.BookID.HasValue && parameters.BookID != 0)
            {
                query = query.Where(b => b.BookID == parameters.BookID);
            }
            

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(b => 
                    b.Book.Title.Contains(parameters.SearchTerm)
                    );
            }

            return query.ToPagedListAsync(parameters.PageNumber,parameters.PageSize);

        }
         


    }
}
