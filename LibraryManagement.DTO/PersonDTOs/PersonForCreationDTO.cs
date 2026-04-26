using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.PersonDTOs
{
    public class PersonForCreationDTO
    {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required string NationalNumber {get; set; } = null!;
            public required string Phone { get; set; }
            public required string Email { get; set; }
            public required string Address { get; set; }
            public required string City { get; set; }
            public required char Gender { get; set; }
    }
}
