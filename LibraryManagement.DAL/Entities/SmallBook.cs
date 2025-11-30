using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // يمكن أن تحتاجها لاحقاً

namespace LibraryManagement.DAL.Entities
{
    [Table("SmallBooks")]
    public class SmallBookEntity
    {
        [Key] 
        public int BookID { get; set; }

        public string Title { get; set; } = null!;
    }
}