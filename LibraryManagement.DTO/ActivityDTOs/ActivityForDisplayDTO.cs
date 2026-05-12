using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.ActivityDTOs
{
    public class ActivityForDisplayDTO <TId> : ActivityBaseDto<TId>
    {
        public required int ActivityID { get; set; }
        
    }
}
