
using LibraryManagement.Domain.TenantContract;

namespace LibraryManagement.Domain.Entities
{
    public class GlobalSettings : IMustHaveTenant
    {
            public int SettingsID { get; set; } = 1;
            public Guid TenantID { get; set; }
            public string Name { get; set; } = "LibCore";
            public decimal DefaultFinePerDay { get; set; } = 0.5m;
            public decimal MaxFineLimit { get; set; } = 100m;
            public int DefaultBorrowingDays { get; set; } = 14;
            public int MaxBooksPerMember { get; set; } = 5;
            public bool IsLibraryOpen { get; set; } = true;
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
            public string? UpdatedBy { get; set; }
        }
    }


