using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Common
{
    public enum LoginResult
    {
       
        Success,
        InvalidCredentials,
        Blocked,
        Deactivated
    }
}

