using LibraryManagement.DTO.GlobalSettings;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.TenantDTOs;
using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.DTO.Owner
{
    public class OwnerRegistrationDTO
    {
        public PersonForCreationDTO Person { get; set; } = null!;
        public UserForAdminCreationDTO AdminUser { get; set; } = null!;
        public TenantForCreationDTO Tenant { get; set; } = null!;
        public GlobalSettingsForCreationDTO? Settings { get; set; }
    }
}
