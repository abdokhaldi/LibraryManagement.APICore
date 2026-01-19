using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.LoginResult;


namespace LibraryManagement.BLL
{

    public class AuthService : IAuthService
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ISecurityService _securityService;
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, ISecurityService securityService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _securityService = securityService;
        }

        public async Task<LoginResult> LoginAsync(string identifier, string password)
        {
            var userForLogin = await _unitOfWork.UserRepository.GetUserForLoginAsync(identifier);
            if (userForLogin == null)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }
            bool isPasswordCorrect = _securityService.Verify(password,userForLogin.Password);
            if (!isPasswordCorrect)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials) ;
            }

            if (userForLogin.IsBlocked == true)
                    {
                return LoginResult.Failure(LoginStatus.Blocked);
                    }

                    if (userForLogin.IsActive == false)
                    {
                        return LoginResult.Failure(LoginStatus.Deactivated);
                    }

            string token = _tokenService.GenerateToken(userForLogin);
            string refreshTokenString = _tokenService.GenerateRefreshToken();
            var newRefreshTokenEntity = new RefreshToken
            {
                Token = refreshTokenString,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserID = userForLogin.UserID
            };
            userForLogin.RefreshTokens.Add(newRefreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();
            return LoginResult.Success(
                new LoginSuccessDTO
                {
                    Token = token,
                    RefreshToken = refreshTokenString,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60)
                }
                );
            }
        
        }

    }

