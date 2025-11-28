using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO
{
    public class BorrowingInfoDTO
    {
      public int BorrowingID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
      public string FullName { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime  DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string  Status { get; set; }
    }
}
