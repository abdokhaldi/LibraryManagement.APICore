using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.ActivityDTOs
{
    public class ActivityForCreationDTO
    {
        public string? ActivityType { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Username { get; set; }
        public string? EntityName { get; set; }
        public required int EntityID { get; set; }
    }
}
