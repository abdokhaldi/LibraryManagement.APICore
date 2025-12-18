using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.DAL.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = null!;
        [MaxLength(100)]
        public string? Publisher { get; set; }

        public string? YearPublished { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public Category? Category { get; set; }
        
        [MaxLength(150)]
        [Column("Image")]
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
