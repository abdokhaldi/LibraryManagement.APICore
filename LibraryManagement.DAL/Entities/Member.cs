using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DAL.Entities
{
    [Table("Members")]
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        [Required]
        public int PersonID { get; set; }
        [ForeignKey(nameof(PersonID))]
        public Person Person { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
    }
}
