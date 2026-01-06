using LibraryManagement.DTO.Common;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace LibraryManagement.DTO.LoginResult
{
    public class LoginResult
    {
        public LoginStatus status { get; }
        public LoginSuccessDTO? Data { get; }

        private LoginResult(LoginStatus status, LoginSuccessDTO? loginSuccessDTO)
        {
            this.status = status;
            this.Data = loginSuccessDTO;
        }

        public static LoginResult Success(LoginSuccessDTO data)
            => new (LoginStatus.Success,data);

        public static LoginResult Failure(LoginStatus status)
        => new (status, null);
    }
}
