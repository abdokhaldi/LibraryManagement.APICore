using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForDisplayDTO
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public string? Publisher { get; set; }
        public string? YearPublished { get; set; }
        public required int Quantity { get; set; }
        public required string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public required bool IsActive { get; set; }

    }
}
