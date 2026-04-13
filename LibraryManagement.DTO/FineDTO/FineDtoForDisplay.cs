using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.FineDTO
{
    public class FineDtoForDisplay
    {
        public int FineID { get; set; }
        public int BorrowingID { get; set; }
        public int MemberID { get; set; }
        public DateTime PaidAt { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;


    }
}
