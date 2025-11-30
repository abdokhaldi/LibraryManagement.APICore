using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Entities
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }= null!;
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(1)]
        public char Gender { get; set; }
        public bool IsActive { get; set; }
        
    }
}
