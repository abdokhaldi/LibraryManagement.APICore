using LibraryManagement.Shared.Parameters.Base;

namespace LibraryManagement.Shared.Parameters
{
    public class BookParameters : RequestParameters
    {
        public int? MemberID { get; set; }
        public string? Status { get; set; }
        public int? CategoryID { get; set; }
    }
}
