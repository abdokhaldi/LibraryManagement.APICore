using LibraryManagement.Shared.Types;
using System.ComponentModel.DataAnnotations;


namespace LibraryManagement.DTO.BookCopyDTO
{
    public class BookCopyForCreationDTO
    {
        [Required]
        public string Barcode { get; set; } = null!;

        [Required]
        public int BookID { get; set; } 
        public CopyStatus Status { get; set; } = CopyStatus.Available;
        public string Condition { get; set; } = "New";
    }
}
