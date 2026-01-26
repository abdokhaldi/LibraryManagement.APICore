using LibraryManagement.Shared.Parameters.Base;

namespace LibraryManagement.Shared.Parameters
{
    public class BookParameters : RequestParameters
    {
        public int? CategoryID { get; set; }
    }
}
