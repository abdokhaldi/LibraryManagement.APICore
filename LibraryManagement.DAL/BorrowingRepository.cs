
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

            var borrowing = await _context.Borrowings
                                    .AsNoTracking()
                                    .Include(b => b.BookCopy)
                                    .ThenInclude(b => b.Book)
                                    .ThenInclude(b => b.Category)
                                    .Include(m => m.Member)
                                    .ThenInclude(m=>m.Person)
                                    .FirstOrDefaultAsync(b => b.BorrowingID == borrowingID);
            return borrowing;
        }

        public  Task<PagedList<Borrowing>> GetBorrowingsAsync(BorrowingParameters parameters)
        {
            var query = _context.Borrowings.AsNoTracking()
                                 .Include(b => b.BookCopy)
                                 .ThenInclude(bc => bc.Book)
                                 .Include(b => b.Member)
                                 .ThenInclude(p => p.Person)
                                 .AsQueryable();

            query = query.Where(b => b.IsCanceled == parameters.IsCanceled);
            

            if (parameters.MemberID.HasValue && parameters.MemberID != 0)
            {
                query = query.Where(b=> b.MemberID == parameters.MemberID);
            }

            if (parameters.BookCopyID.HasValue && parameters.BookCopyID != 0)
            {
                query = query.Where(b => b.BookCopyID == parameters.BookCopyID);
            }
            

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(b => 
                    b.BookCopy.Book!.Title.Contains(parameters.SearchTerm)
                    );
            }

            return query.ToPagedListAsync(parameters.PageNumber,parameters.PageSize);

        }
         


    }
}
