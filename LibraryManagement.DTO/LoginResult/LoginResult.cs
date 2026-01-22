using LibraryManagement.DTO.Common;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace LibraryManagement.DTO.LoginResult
{
    public class LoginResult
    {
        public LoginStatus status { get; }
        public LoginSuccessDTO? Data { get; }
        public string Message { get; set; } = string.Empty;
        private LoginResult(LoginStatus status, LoginSuccessDTO? loginSuccessDTO)
        {
            this.status = status;
            this.Data = loginSuccessDTO;
        }
        private LoginResult(LoginStatus status, string message)
        {
            this.status = status;
            this.Message = message;
        }

        public static LoginResult Success(LoginSuccessDTO data)
            => new (LoginStatus.Success,data);
        public static LoginResult Success(string message="")
            => new(LoginStatus.Success, message);

        public static LoginResult Failure(LoginStatus status)
        => new (status, string.Empty);
    }
}
