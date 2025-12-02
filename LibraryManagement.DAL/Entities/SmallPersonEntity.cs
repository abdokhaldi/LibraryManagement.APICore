using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace LibraryManagement.DAL.Entities
{
    [Table("SmallPeople")]
    public class SmallPersonEntity
    {
        [Key]
        public int PersonID { get; set; }

        public string FullName { get; set; } = null!;
    }
}
