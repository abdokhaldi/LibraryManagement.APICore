using LibraryManagement.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.DAL.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string Password { get; set; } = null!;
        public int RoleID { get; set; }
        public Role Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBlocked { get; set; }
    }
}
