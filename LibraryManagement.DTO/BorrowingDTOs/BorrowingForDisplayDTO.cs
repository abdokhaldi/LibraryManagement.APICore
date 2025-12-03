using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BorrowingDTOs
{
    public class BorrowingForDisplayDTO
    {
       
            public required int BorrowingID { get; set; }
            public required string Title { get; set; }
            public required string FullName { get; set; }
            public required DateTime BorrowingDate { get; set; }
            public required DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public required string Status { get; set; }
    }
}
