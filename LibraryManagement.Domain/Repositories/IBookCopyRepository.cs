using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

using System.Linq.Expressions;

namespace LibraryManagement.Domain.Repositories
{
   public interface IBookCopyRepository
    {
        Task<PagedList<BookCopy>> GetActiveBookCopiesAsync(BookCopyParameters parameters);

        Task<BookCopy?> GetBookCopyAsync(Expression<Func<BookCopy, bool>> predicate, bool trackChanges=false);
     
        Task AddNewBookCopyAsync(BookCopy bookCopyEntity);

        Task<int> GetAvailableBookCopiesQuantityAsync(int bookID);
    }
}
