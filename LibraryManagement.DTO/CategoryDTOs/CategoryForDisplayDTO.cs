using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.CategoryDTOs
{
    public class CategoryForDisplayDTO
    {
        public required int CategoryID { get; set; }
        public required string CategoryName { get; set; }

    }
}
