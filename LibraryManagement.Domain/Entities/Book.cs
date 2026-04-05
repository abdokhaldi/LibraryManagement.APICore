

namespace LibraryManagement.Domain.Entities
{

    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Publisher { get; set; } = null!; 
        public short YearPublished { get; set; }
        public int CategoryID { get; set; } 
        public bool IsActive { get; set; } = true; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ImagePath { get; set; } = "image.jpg";
        public Category Category { get; set; } = null!;

        public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

        public int TotalQuantity => BookCopies?.Count(c => c.IsActive) ?? 0;
    }
}

    
