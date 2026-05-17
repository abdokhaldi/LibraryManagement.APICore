

namespace LibraryManagement.DTO.GlobalSettings
{
    public class GlobalSettingsForCreationDTO
    {
        public decimal? DefaultFinePerDay { get; set; }
        public decimal? MaxFineLimit { get; set; }
        public int? DefaultBorrowingDays { get; set; }
        public int? MaxBooksPerMember { get; set; }
        public bool? IsLibraryOpen { get; set; } = true;
        public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;
        public string? DefaultLanguage { get; set; }
        public string? TimeZone { get; set; }
        
    }
}
