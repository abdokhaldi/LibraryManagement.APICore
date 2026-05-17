

namespace LibraryManagement.Domain.Entities.Tenants
{
        public class Tenant
        {
            public Guid TenantID { get; set; }
            public string Name { get; set; } = null!;
            public string Identifier { get; set; } = null!;
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public GlobalSettings GlobalSettings { get; set; } = null!;
            public ICollection<User> Users { get; set; } = new List<User>();
      
        
    }
    }

