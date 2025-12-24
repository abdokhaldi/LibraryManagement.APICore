using LibraryManagement.BLL.Common;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        
        Task<(LoginResult status, string message)> LoginAsync(string identifier, string password);
    }
}
