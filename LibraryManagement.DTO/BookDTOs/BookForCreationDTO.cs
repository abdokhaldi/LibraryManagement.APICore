using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForCreationDTO
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required string YearPublished { get; set; }
        public required int Quantity { get; set; }
        public required int CategoryID { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? Image { get; set; }

    }
}
