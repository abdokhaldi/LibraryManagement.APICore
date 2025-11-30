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
        public int PersonID { get; set; }
        public Person Person { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
    }
}
