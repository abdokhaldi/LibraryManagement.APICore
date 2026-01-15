

namespace LibraryManagement.Domain.Entities
{
    
    public class Activity
    {
        public int  ActivityID { get; set; }
        public string ActivityType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string EntityName { get; set; } = null!;
        public int EntityID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; } = null!;
    }
}
