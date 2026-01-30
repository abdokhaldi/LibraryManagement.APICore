using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserForUpdateAsync(int userID);
        Task<User?> GetUserForReadOnlyAsync(int userID);

        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserForLoginAsync(string identifier);
        
        Task<PagedList<User>> GetActiveUsersAsync(UserParameters parameters);

        Task<bool> IsUsernameExistsAsync(string username);
        Task<bool> IsUsernameExistsForUpdateAsync(int id,string username);

        Task AddNewUserAsync(User userEntity);

        Task UpdateUserAsync(User userEntity);
        Task<User?> GetUserAsPersonAsync(int personID);

        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);

    }
}