using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.PersonDTOs
{
    public class PersonForDisplayDTO
    {
        public required int PersonID { get; set; }
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Gender { get; set; }
        public required bool IsActive { get; set; }
    }


}
