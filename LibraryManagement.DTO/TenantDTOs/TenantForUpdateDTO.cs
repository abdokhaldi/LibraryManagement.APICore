

namespace LibraryManagement.DTO.TenantDTOs
{
    public class TenantForUpdateDTO
    {
        public string? Name { get; set; } = null!;
        public string? Identifier { get; set; } = null!;
        public string? DefaultLanguage { get; set; }
        public string? TimeZone { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
