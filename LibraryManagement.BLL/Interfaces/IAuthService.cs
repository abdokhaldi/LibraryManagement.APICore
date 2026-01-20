using LibraryManagement.DTO.LoginResult;
using LibraryManagement.DTO.RefreshTokenDTOs;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        
        Task<LoginResult> LoginAsync(string identifier, string password);
        Task<LoginResult> RefreshTokenAsync(RefreshTokenRequestDTO tokenDTO);
    }
}
