using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.MemberDTOs
{
    public class MemberForDisplayDTO
    {
        public required int MemberID { get; set; }
        public required string FullName { get; set; }
        public required DateTime JoinDate { get; set; }
        public required bool IsActive { get; set; }
    }
}
