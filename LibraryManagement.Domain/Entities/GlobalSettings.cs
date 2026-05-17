
using LibraryManagement.Domain.TenantContract;

namespace LibraryManagement.Domain.Entities
{
    public class GlobalSettings : IMustHaveTenant
    {
            public Guid SettingsID { get; set; }
            public Guid TenantID { get; set; }
            public decimal DefaultFinePerDay { get; set; } = 0.5m;
            public decimal MaxFineLimit { get; set; } = 100m;
            public int DefaultBorrowingDays { get; set; } = 14;
            public int MaxBooksPerMember { get; set; } = 5;
            public bool IsLibraryOpen { get; set; } = true;
            public string DefaultLanguage { get; set; } = "en";
            public string TimeZone { get; set; } = "UTC";
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
            public string? UpdatedBy { get; set; }
        }
    }


