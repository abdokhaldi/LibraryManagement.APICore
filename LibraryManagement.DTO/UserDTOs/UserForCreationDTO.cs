

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForCreationDTO
    {
        public required Guid PersonID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int RoleID { get; set; }
        public required string RoleName { get; set; }
    }

    public class UserForAdminCreationDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int RoleID { get; set; }
     }
}
