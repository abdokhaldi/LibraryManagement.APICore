using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BorrowingDTOs
{
    public class BorrowingForCreationDTO
    {
        
            public required int BookID { get; set; }
            public required int PersonID { get; set; }
            public required DateTime DueDate { get; set; }
        }

    }

