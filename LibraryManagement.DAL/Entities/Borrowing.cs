using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.DAL.Entities
{
    [Table("Borrowings")]
    public class Borrowing
    {
        [Key]
        public int BorrowingID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; } = null!;
        public int PersonID { get; set; }
        public Person Person { get; set; } = null!;
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;
        public bool IsCanceled { get; set; }
    }
}
