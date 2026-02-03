
using LibraryManagement.Shared.Types;

namespace LibraryManagement.DTO.BookCopyDTO
{
    
        public class BookCopyForUpdateDTO
        {
           
            public CopyStatus? Status { get; set; }

            
            public string? Condition { get; set; }

            public bool? IsActive { get; set; }
        }
    }

