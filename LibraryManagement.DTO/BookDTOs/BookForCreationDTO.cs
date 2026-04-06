
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
 

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForCreationDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title length must be between 3 and 100 characters.")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage ="ISBN should have 13 digits")]
        public string ISBN { get; set; } = null!;
        public string Description { get; set; } = null!;
        public required string Author { get; set; }
        public string Publisher { get; set; } = null!;
        public short YearPublished { get; set; }
        public required int CategoryID { get; set; }
        public IFormFile? Image { get; set; }

    }
}
