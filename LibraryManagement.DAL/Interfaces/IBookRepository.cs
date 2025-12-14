using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
   
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetQueryableBooksAsync();

        Task<Book?> GetBookForUpdateAsync(int bookID);
        Task<Book?> GetBookForReadOnlyAsync(int bookID);
        Task<bool> IsTitleExistsAsync(string title);
        Task<bool> IsTitleExistsAsync(int id,string title);

        Task AddNewBookAsync(Book bookEntity);

        Task<int?> GetBookQuantityAsync(int bookID);

    }
}