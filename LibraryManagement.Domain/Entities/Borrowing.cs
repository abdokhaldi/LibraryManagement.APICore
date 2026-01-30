

namespace LibraryManagement.Domain.Entities
{
   
    public class Borrowing
    {
        

        public int BorrowingID { get; set; }

      
        public int BookID { get; set; }
       

        public int MemberID { get; set; }
        public DateTime BorrowingDate { get; set; }
       
        public DateTime DueDate { get; set; }
       
        public DateTime? ReturnDate { get; set; }
       
        public string Status { get; set; } = null!;
       
        public bool IsCanceled { get; set; }
        

        public Book Book { get; set; } = null!;    
        public Member Member { get; set; } = null!;

        
    }
}
