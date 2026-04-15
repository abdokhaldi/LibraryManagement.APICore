using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.GlobalSettings
{
    public class GlobalSettingsForReadOnlyDTO
    {
        public decimal DefaultFinePerDay { get; set; }
        public decimal MaxFineLimit { get; set; }
        public int DefaultBorrowingDays { get; set; }
        public int MaxBooksPerMember { get; set; }
        public bool IsLibraryOpen { get; set; } = true;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
    }
}
