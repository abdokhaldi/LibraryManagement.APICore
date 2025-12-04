using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.SmallPersonDTO
{
    public class SmallPersonForDisplayDTO
    {
      public required int PersonID { get; set; }
        public required string FullName { get; set; }
    }
}
