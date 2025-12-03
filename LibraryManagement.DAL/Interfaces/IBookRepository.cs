using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    // تعريف الواجهة IBookRepository
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetQueryableBooksAsync();

        Task<Book?> GetBookForUpdateAsync(int bookID);
        Task<Book?> GetBookForReadOnlyAsync(int bookID);

        Task AddNewBookAsync(Book bookEntity);

        Task UpdateBookAsync(Book bookEntity);

        Task<int?> GetBookQuantityAsync(int bookID);

    }
}