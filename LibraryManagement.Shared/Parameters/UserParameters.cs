

using LibraryManagement.Shared.Parameters.Base;

namespace LibraryManagement.Shared.Parameters
{
    public class UserParameters : RequestParameters
    {
        public Guid? PersonID { get; set; }
        public bool IsBlocked { get; set; } = false;

    }
}
