using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.BookDTOs
{
    public class BookForUpdateDTO
    {
      //  [Required(ErrorMessage = "The ID cannot be null")]
      //  public int BookID { get; set; }
        [StringLength(100,MinimumLength =3,ErrorMessage = "Title must be at lest 3 characters long")]
        public  string? Title { get; set; }
        public  string? Author { get; set; }
        public  string? Publisher { get; set; }
        public  string? YearPublished { get; set; }
        [Range(1,10000,ErrorMessage = "Quantity must be between 1 to 10000")]
        public  int? Quantity { get; set; }
        public  int? CategoryID { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsActive { get; set; }

    }
}
