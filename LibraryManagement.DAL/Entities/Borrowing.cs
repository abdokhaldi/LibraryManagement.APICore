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

        [Required]
        public int BookID { get; set; }
        [ForeignKey(nameof(BookID))]
        public Book Book { get; set; } = null!;
        [Required]
        public int MemberID { get; set; }
        [ForeignKey(nameof(MemberID))]
        public Member Member { get; set; } = null!;
        
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;
        public bool IsCanceled { get; set; }
    }
}
