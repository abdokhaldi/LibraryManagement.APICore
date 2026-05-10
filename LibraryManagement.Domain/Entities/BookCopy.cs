using LibraryManagement.Domain.TenantContract;
using LibraryManagement.Shared.Types;

namespace LibraryManagement.Domain.Entities
{
   
        
        public class BookCopy : IMustHaveTenant
    {
            public int BookCopyID { get; set; }
            public Guid TenantID { get; set; }
           
           public string Barcode { get; set; } = null!;

            public CopyStatus Status { get; set; } = CopyStatus.Available;

            public string? Condition { get; set; }
           
            public bool IsActive { get; set; } = true;

            public int BookID { get; set; }
            public Book? Book { get; set; }
 
            public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
            
             
    }
    }

