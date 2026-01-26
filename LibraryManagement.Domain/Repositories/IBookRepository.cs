using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.Domain.Interfaces
{
   
    public interface IBookRepository
    {
        public IQueryable<Book> GetBookQuery(BookParameters parameters);

        Task<Book?> GetBookForUpdateAsync(int bookID);
        Task<Book?> GetBookForReadOnlyAsync(int bookID);
        Task<bool> IsTitleExistsAsync(string title);
        Task<bool> IsTitleExistsAsync(int id,string title);

        Task AddNewBookAsync(Book bookEntity);

        Task<int> GetBookQuantityAsync(int bookID);

    }
}