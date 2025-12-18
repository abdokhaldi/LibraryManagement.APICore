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
        [Required]
        public int PersonID { get; set; }
        [ForeignKey(nameof(PersonID))]
        public Person Person { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string Password { get; set; } = null!;
        [Required]
        public int RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBlocked { get; set; }
    }
}
