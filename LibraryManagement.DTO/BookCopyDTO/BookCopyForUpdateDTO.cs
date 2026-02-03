
using LibraryManagement.Shared.Types;

namespace LibraryManagement.DTO.BookCopyDTO
{
    
        public class BookCopyForUpdateDTO
        {
           
            public CopyStatus? Status { get; set; }

            
            public string? Condition { get; set; } = null;

            public bool? IsActive { get; set; }
        }
    }

