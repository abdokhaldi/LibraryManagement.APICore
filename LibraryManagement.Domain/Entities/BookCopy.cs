

namespace LibraryManagement.Domain.Entities
{
   
        public enum CopyStatus
        {
            Available = 1, 
            Borrowed = 2,  
            Lost = 3,      
            Damaged = 4,   
            Reserved = 5   
        }

        public class BookCopy
        {
            public int BookCopyID { get; set; }

        public string Barcode { get; set; } = null!;

            public CopyStatus Status { get; set; } = CopyStatus.Available;

            public string? Condition { get; set; }
           
            public bool IsActive { get; set; } = true;

            public int BookId { get; set; }
            public Book Book { get; set; } = null!;
 
            public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
            
             
    }
    }

