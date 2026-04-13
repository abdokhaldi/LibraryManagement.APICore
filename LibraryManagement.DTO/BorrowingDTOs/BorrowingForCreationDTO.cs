using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BorrowingDTOs
{
    public class BorrowingForCreationDTO
    {
            
            public required int BookCopyID { get; set; }
            public required int PersonID { get; set; }
            
            public required DateTime DueDate { get; set; }
        }

    }

