using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.TenantDTOs
{
    public class TenantForCreationDTO
    {
        
        public string Name { get; set; } = null!;
        public string Identifier { get; set; } = null!;
        public string? DefaultLanguage { get; set; }
        public string? TimeZone { get; set; }
    }
}
