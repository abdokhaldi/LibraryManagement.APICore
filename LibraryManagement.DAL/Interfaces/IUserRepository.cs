using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIDAsync(int userID);

        Task<User?> GetUserByUsernameAsync(string username);

        Task<IQueryable<User>> GetQueryableUsersAsync();

        Task<bool> IsUsernameExistsAsync(string username);

        Task<int> AddNewUser(User userEntity);

        Task<int> UpdateUserAsync(User userEntity);

        Task<int> SetUserActiveStatusAsync(int userID, bool isActive);

        Task<int> SetUserBlockStatusAsync(int userID, bool isBlocked);
    }
}