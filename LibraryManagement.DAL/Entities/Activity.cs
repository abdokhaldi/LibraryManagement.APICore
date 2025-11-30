using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LibraryManagement.DAL.Entities
{
    [Table("Activities")]
    public class Activity
    {
        [Key]
        public int ActivityID { get; set; }
        public string? ActivityType { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Username { get; set; }
        public string? EntityName { get; set; }
        public int EntityID { get; set; }
    }
}
