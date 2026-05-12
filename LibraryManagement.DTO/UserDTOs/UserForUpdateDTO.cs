

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForUpdateDTO
    {
        public Guid? PersonID { get; set; }
        public string? Username { get; set; }
        public  int? RoleID { get; set; }
        public  bool? IsActive { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
