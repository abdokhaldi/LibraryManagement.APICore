

namespace LibraryManagement.Domain.Entities
{
    
        public class RefreshToken
        {
            public int ID { get; set; }
            public string Token { get; set; } = string.Empty;

            public DateTime Expires { get; set; }

            public DateTime Created { get; set; }

            public bool IsUsed { get; set; }

            public DateTime? Revoked { get; set; }
            
            
            public bool IsExpired => DateTime.UtcNow >= Expires;

            public bool IsActive => Revoked == null && !IsExpired;
            
            public int UserID { get; set; }
            public User User { get; set; } = null!;
        }
    }

