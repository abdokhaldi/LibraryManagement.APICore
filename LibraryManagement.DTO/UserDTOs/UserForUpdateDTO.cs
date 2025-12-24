using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForUpdateDTO
    {
        public  int? PersonID { get; set; }
        public  string? Username { get; set; }
        public  int? RoleID { get; set; }
        public  bool? IsActive { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
