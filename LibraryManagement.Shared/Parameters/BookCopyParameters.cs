using LibraryManagement.Shared.Parameters.Base;
using LibraryManagement.Shared.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Shared.Parameters
{
    public class BookCopyParameters : RequestParameters
    {
     public int? BookID { get; set; }
        public CopyStatus? Status { get; set; } = null;
        public bool? IsActive { get; set; } = null;


    }
}
