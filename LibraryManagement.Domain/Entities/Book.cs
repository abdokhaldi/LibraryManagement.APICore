

namespace LibraryManagement.Domain.Entities
{
   
    public class Book
    {
       
        public int BookID { get; set; }
        
        public string Title { get; set; } = null!;
        
        public string Author { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public short? YearPublished { get; set; }
        
        public short Quantity { get; set; }
        
        public int CategoryID { get; set; }

        public Category Category { get; set; } = null!;
       
        public string? ImagePath { get; set; }
       
        public bool IsActive { get; set; }
    }
}
