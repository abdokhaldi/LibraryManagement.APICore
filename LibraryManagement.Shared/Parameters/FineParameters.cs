using LibraryManagement.Shared.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Shared.Parameters
{
    public class FineParameters : RequestParameters
    {
        public int? MemberID { get; set; }
        public int? BorrowingID {get;set;}
        public string? Status { get; set; }
    }
}
