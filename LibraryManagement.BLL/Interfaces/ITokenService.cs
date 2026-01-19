using LibraryManagement.Domain.Entities;

namespace LibraryManagement.BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
