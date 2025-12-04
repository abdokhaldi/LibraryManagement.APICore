using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.MemberDTOs
{
    public class MemberForUpdateDTO
    {
        public required int MemberID { get; set; }
        public required int PersonID { get; set; }
        public required bool IsActive { get; set; }
    }
}
