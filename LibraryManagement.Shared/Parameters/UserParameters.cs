

using LibraryManagement.Shared.Parameters.Base;

namespace LibraryManagement.Shared.Parameters
{
    public class UserParameters : RequestParameters
    {
        public int? PersonID { get; set; }
        public bool IsBlocked { get; set; } = false;

    }
}
