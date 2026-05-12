

namespace LibraryManagement.Domain.Entities.Tenants
{
        public class Tenant
        {
            public Guid TenantID { get; set; }
            public string Name { get; set; } = null!;
            public string Identifier { get; set; } = null!;
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public string DefaultLanguage { get; set; } = "en";
            public string TimeZone { get; set; } = "UTC";
            public GlobalSettings GlobalSettings { get; set; } = null!;
            public ICollection<User> Users { get; set; } = new List<User>();
        }
    }

