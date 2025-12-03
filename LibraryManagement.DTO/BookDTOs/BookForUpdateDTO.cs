using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BookDTOs
{
    internal class BookForUpdateDTO
    {
        public required int BookID { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required string YearPublished { get; set; }
        public required int Quantity { get; set; }
        public required int CategoryID { get; set; }
        public string? ImagePath { get; set; }
        public required bool IsActive { get; set; }

    }
}
