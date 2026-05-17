


using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Domain.TenantContract;

namespace LibraryManagement.Domain.Entities
{
    public class User : IMustHaveTenant
    {
        public Guid UserID { get; set; }
        public Guid TenantID { get; set; } 
        public Guid PersonID { get; set; }
       
        public Person Person { get; set; } = null!;
        
        public string Username { get; set; } = null!;
        
        public string Password { get; set; } = null!;
       
        public int RoleID { get; set; }
        
        public Role Role { get; set; } = null!;
        
        public bool IsActive { get; set; }
       
        public DateTime CreatedAt { get; set; }
        
        public bool IsBlocked { get; set; }
        
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
