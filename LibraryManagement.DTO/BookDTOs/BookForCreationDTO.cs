using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForCreationDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title length must be between 3 and 100 characters.")]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public required string Author { get; set; }
        public string Publisher { get; set; } = null!;
        public short YearPublished { get; set; }
        public required int CategoryID { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? Image { get; set; }

    }
}
