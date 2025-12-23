using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.UserDTOs
{
    public class UserForDisplayDTO
    {
        public required int UserID { get; set; }
        public required string FullName { get; set; }
        public required string Username { get; set; }
        public required string RoleName { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
