using LibraryManagement.Shared.Types;
using System.ComponentModel.DataAnnotations;


namespace LibraryManagement.DTO.BookCopyDTO
{
    public class BookCopyForCreationDTO
    {
        public string? Barcode { get; set; }

        [Required]
        public int BookID { get; set; } 
        public CopyStatus Status { get; set; } = CopyStatus.Available;
        public string Condition { get; set; } = "New";
        
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; } = 1;
    }
}
