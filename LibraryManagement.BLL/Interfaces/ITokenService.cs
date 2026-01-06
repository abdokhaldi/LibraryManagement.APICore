using LibraryManagement.DAL.Entities;

namespace LibraryManagement.BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
