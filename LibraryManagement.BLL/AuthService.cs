using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.AuthDTOs;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.LoginResult;
using LibraryManagement.DTO.Owner;
using LibraryManagement.DTO.RefreshTokenDTOs;


namespace LibraryManagement.BLL
{

    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, ISecurityService securityService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<LoginResult> LoginAsync(string identifier, string password)
        {
            var userForLogin = await _unitOfWork.UserRepository.GetUserForLoginAsync(identifier);
            if (userForLogin == null)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }
            bool isPasswordCorrect = _securityService.Verify(password, userForLogin.Password);
            if (!isPasswordCorrect)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
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
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15)
                }
                );
        }

        public async Task<LoginResult> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user == null)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }

            var storedToken = user.RefreshTokens.SingleOrDefault(t => t.Token == request.RefreshToken);

            if (storedToken == null || !storedToken!.IsActive)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }

            if (user.IsBlocked)
            {
                return LoginResult.Failure(LoginStatus.Blocked);
            }

            if (!user.IsActive)
            {
                return LoginResult.Failure(LoginStatus.Deactivated);
            }

            storedToken.Revoked = DateTime.UtcNow;
            storedToken.IsUsed = true;

            var newAccessToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserID = user.UserID
            };

            user.RefreshTokens.Add(newRefreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return LoginResult.Success(
                new LoginSuccessDTO
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(15)
                }
              );
        }

        public async Task<LoginResult> LogoutAsync(LogoutRequestDTO requestDTO)

        {
            var user = await _unitOfWork.UserRepository.GetUserByRefreshTokenAsync(requestDTO.RefreshToken);
            if (user == null)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }
            var storedToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == requestDTO.RefreshToken);
            if (storedToken == null || !storedToken.IsActive)
            {
                return LoginResult.Failure(LoginStatus.InvalidCredentials);
            }
            storedToken.Revoked = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
            return LoginResult.Success();
        }

       
    }

    }

