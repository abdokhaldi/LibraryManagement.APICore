using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Domain.Entities
{
    public class Role
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; } = null!;
    }
}
