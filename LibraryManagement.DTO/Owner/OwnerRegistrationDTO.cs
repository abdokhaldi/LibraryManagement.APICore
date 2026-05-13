using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.TenantDTOs;
using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.DTO.Owner
{
    public class OwnerRegistrationDTO
    {
        public PersonForCreationDTO Person { get; set; } = null!;
        public UserForCreationDTO User { get; set; } = null!;
        public TenantForCreationDTO Tenant { get; set; } = null!;
    }
}
