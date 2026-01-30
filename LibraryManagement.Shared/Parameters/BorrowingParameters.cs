

using LibraryManagement.Shared.Parameters.Base;

namespace LibraryManagement.Shared.Parameters
{
    public class BorrowingParameters : RequestParameters
    {
        public int? MemberID { get; set; }
        public int? BookID {get;set;}
        public bool IsCanceled { get; set; } = false;

    }
}
