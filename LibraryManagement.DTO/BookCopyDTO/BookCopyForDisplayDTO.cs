

namespace LibraryManagement.DTO.BookCopyDTO
{
    public class BookCopyForDisplayDTO
    {
        public int BookCopyID { get; set; }
        public string Barcode { get; set; } = null!;
        public string Status { get; set; } = null!; 
        public string Condition { get; set; } = null!; // حالة النسخة (New, Good, Damaged)

        
        public int BookID { get; set; }
        public string BookTitle { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string DateAdded { get; set; } = null!;
        public bool CanBeBorrowed { get; set; }
    }
}
