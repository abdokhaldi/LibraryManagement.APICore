

namespace LibraryManagement.BLL.Interfaces
{
    public interface ISecurityService
    {
        public string HashPassword(string password);
        public bool Verify(string password, string hashedPassword);
        
    }
}
