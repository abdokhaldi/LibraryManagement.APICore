using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    // تعريف الواجهة IBookRepository
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetQueryableBooksAsync();

        Task<Book?> GetBookByIDAsync(int bookID);

        Task<int> AddNewBookAsync(Book bookEntity);

        Task<int> UpdateBookAsync(Book bookEntity);

        Task<int?> GetBookQuantityAsync(int bookID);

        Task<int> SetBookStatusAsync(int bookID, bool isActive);
    }
}