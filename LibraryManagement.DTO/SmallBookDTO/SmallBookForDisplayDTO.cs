using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.SmallBookDTO
{
    public class SmallBookForDisplayDTO
    {
        public required int BookID { get; set; }
        public required string Title { get; set; }
    }
}
