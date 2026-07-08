using LibraryManagement.DTO.PersonDTOs;

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForUpdateDTO
    {
        public Guid? PersonID { get; set; }
        public string? Username { get; set; }
        public  int? RoleID { get; set; }
        public PersonForUpdateDTO? Person { get; set; } = null;

    }
}
