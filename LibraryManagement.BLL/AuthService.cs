using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.BLL.Common;

namespace LibraryManagement.BLL
{

    public class AuthService : IAuthService
    {
       
        private readonly IUnitOfWork _unitOfWork;
        
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(LoginResult status,string message)> LoginAsync(string identifier, string password)
        {
            var userForLogin = await _unitOfWork.UserRepository.GetUserForLoginAsync(identifier);
            if (userForLogin == null)
            {
                return (LoginResult.InvalidCredentials, "Invalid identifier or password .");
            }
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password,userForLogin.Password);
            if (!isPasswordCorrect)
            {
                return (LoginResult.InvalidCredentials, "Invalid identifier or password .");
            }

            if (userForLogin.IsBlocked == true)
                    {
                        return (LoginResult.Blocked, "Your account is blocked, contact the admin.");
                    }

                    if (userForLogin.IsActive == false)
                    {
                        return (LoginResult.Deactivated, "Your account is inactivated, contact the admin.");
                    }

            return (LoginResult.Success, "Welcome , you have successfully logged in .");
            }
        
        }

    }

