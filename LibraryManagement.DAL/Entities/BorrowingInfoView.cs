using System;
using  System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.DAL.Entities
{
    public class BorrowingInfoView
    {
        [Key]
        public int BorrowingID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
