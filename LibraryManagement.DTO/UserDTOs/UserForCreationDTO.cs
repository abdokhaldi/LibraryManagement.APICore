using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForCreationDTO
    {
        public required int PersonID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int RoleID { get; set; }
    }
}
