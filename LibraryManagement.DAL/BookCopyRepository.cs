using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Types;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Base;

namespace LibraryManagement.Domain.Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly LibraryDbContext _context;
        
        public BookCopyRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<BookCopy>> GetActiveBookCopiesAsync(BookCopyParameters parameters)
        {
            var query = _context.BookCopies
                                .IgnoreQueryFilters()
                                .AsNoTracking()
                                .Include(cb => cb.Book)
                                .ThenInclude(cb => cb!.Category)
                                .AsQueryable();
            if (parameters.IsActive.HasValue)
            {
                query = query.Where(cb => cb.IsActive == parameters.IsActive.Value);
            }
            if (parameters.BookID.HasValue)
            {
                query = query.Where(cb => cb.BookID == parameters.BookID);
            }
            if (parameters.Status.HasValue)
            {
                query = query.Where(cb => cb.Status==parameters.Status);
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim();
                query = query.Where(cb => cb.Barcode.Contains(searchTerm));
            }
            query = query.ApplySort(parameters.OrderBy);

            return query.ToPagedListAsync(parameters.PageNumber, parameters.PageSize);
        }
       
       public async Task<BookCopy?> GetBookCopyAsync(Expression<Func<BookCopy, bool>> predicate, bool trackChanges = false) 
        {
            var query =  _context.BookCopies.AsQueryable();

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            query = query.Include(c => c.Book)
                         .ThenInclude(b => b!.Category);

            return await query.FirstOrDefaultAsync(predicate);

        }
       
       public Task AddNewBookCopyAsync(BookCopy bookCopyEntity) {
            _context.BookCopies.Add(bookCopyEntity);
            return Task.CompletedTask;
        }
       
       public  async Task<int> GetAvailableBookCopiesQuantityAsync(int bookID) {
            return await _context.BookCopies
             .Where(bc =>
            bc.BookID == bookID
            && bc.IsActive == true
            && bc.Status == CopyStatus.Available
            ).CountAsync();
        }
    }
}
