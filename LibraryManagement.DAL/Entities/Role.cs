using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.DAL.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
