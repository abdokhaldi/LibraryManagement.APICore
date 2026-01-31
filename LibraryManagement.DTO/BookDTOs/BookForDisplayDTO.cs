

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForDisplayDTO
    {
        public required int BookID { get; set; }
        public required string Title { get; set; }
        public  required string Description{get;set;}
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required string Publisher { get; set; } 
        public required short YearPublished { get; set; }
        public required string CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public required bool IsActive { get; set; }

        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public bool IsAvailable => AvailableCopies > 0;

    }
}
