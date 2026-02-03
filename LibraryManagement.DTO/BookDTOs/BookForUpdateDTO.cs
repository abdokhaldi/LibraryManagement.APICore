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
        [StringLength(100,MinimumLength =3,ErrorMessage = "Title must be at lest 3 characters long")]
        public  string? Title { get; set; }
      //  public string? ISBN { get; set; } 
        public string? Description { get; set; } 
        public  string? Author { get; set; }
        public  string? Publisher { get; set; }
        public short? YearPublished { get; set; }
        public  int? CategoryID { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsActive { get; set; }

    }
}
