
using LibraryManagement.Domain.Entities;
namespace LibraryManagement.Domain.Entities
{
   
    public class Borrowing
    {
        

        public int BorrowingID { get; set; }

      
        public int BookCopyID { get; set; }
       

        public int MemberID { get; set; }
        public DateTime BorrowingDate { get; set; }
       
        public DateTime DueDate { get; set; }
       
        public DateTime? ReturnDate { get; set; }
        
        public decimal InitialFees { get; set; }
        public string Status { get; set; } = null!;
       
        public bool IsCanceled { get; set; }


        public BookCopy BookCopy { get; set; } = null!;
        public Member Member { get; set; } = null!;
        public Fine? Fine { get; set; }

        
    }
}
