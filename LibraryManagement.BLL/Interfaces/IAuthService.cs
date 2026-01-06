using LibraryManagement.DTO.LoginResult;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        
        Task<LoginResult> LoginAsync(string identifier, string password);
    }
}
