

using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.RoleDTOs;

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForDisplayDTO
    {
        public required Guid UserID { get; set; }
        public required string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public required bool IsActive { get; set; }
        public required bool IsBlocked { get; set; }
        public RoleForDisplayDTO? Role { get; set; }
        public PersonForDisplayDTO? Person { get; set; }
        

    }
}
