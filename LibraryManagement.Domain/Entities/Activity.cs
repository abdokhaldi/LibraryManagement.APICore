

using LibraryManagement.Domain.TenantContract;

namespace LibraryManagement.Domain.Entities
{
    
    public class Activity : IMustHaveTenant
    {
        public int  ActivityID { get; set; }
        public Guid TenantID { get; set; }
        public string ActivityType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string EntityName { get; set; } = null!;
        public int EntityID { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; } = null!;
    }
}
