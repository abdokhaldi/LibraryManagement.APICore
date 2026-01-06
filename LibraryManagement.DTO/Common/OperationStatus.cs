using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.Common
{
    public enum OperationStatus
    {
        Success,
        Blocked,
        Deactivated,
        Cancelled,
        Conflict,
        ValidationError,
        NotFound

    }
}
