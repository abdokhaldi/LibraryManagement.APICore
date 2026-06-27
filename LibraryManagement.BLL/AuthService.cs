using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.AuthDTOs;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.LoginResult;
using LibraryManagement.DTO.OperationResults;
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

        public async Task<OperationResult<LoginResult>> AdminRegistrationAsync(OwnerRegistrationDTO ownerDTO)
        {
            Guid newTenantId = Guid.NewGuid();
            Guid newPersonId = Guid.NewGuid();
            Guid newUserId = Guid.NewGuid();
            Guid newSettingId = Guid.NewGuid();

            var tenantDTO = ownerDTO.Tenant;
            var personDTO = ownerDTO.Person;
            var userDTO = ownerDTO.AdminUser;
            var settingDTO = ownerDTO.Settings;

            bool identifierExists = await _unitOfWork.TenantRepository.IsTenantIdentitierExistAsync(tenantDTO.Identifier);
            var (emailExists,phoneExists) = await _unitOfWork.PersonRepository.IsEmailOrPhoneExistsAsync(personDTO.Email, personDTO.Phone);
            bool usernameExists = await _unitOfWork.UserRepository.IsUsernameExistsAsync(userDTO.Username);

            if (identifierExists)
                return OperationResult <LoginResult>.Failure(OperationStatus.Conflict, "The tenant identifier is already exists");
           
            if(emailExists)
                return OperationResult<LoginResult>.Failure(OperationStatus.Conflict, "The email is already exists");
           
            if(phoneExists)
                return OperationResult<LoginResult>.Failure(OperationStatus.Conflict, "The phone number is already exists");
            
            if(usernameExists)
                return OperationResult<LoginResult>.Failure(OperationStatus.Conflict, "The username is already exists");

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var tenantEntity = _mapper.Map<Tenant>(tenantDTO);
                tenantEntity.TenantID = newTenantId;
                tenantEntity.IsActive = true;
                tenantEntity.CreatedAt = DateTime.UtcNow;
                
          var settingEntity = _mapper.Map<GlobalSettings>(settingDTO);
                settingEntity.SettingsID = newSettingId;
                settingEntity.TenantID = newTenantId;

                
           var personEntity = _mapper.Map<Person>(personDTO);
                personEntity.PersonID = newPersonId;
                personEntity.TenantID = newTenantId;
                personEntity.IsActive = true;
                
                string hashedPassword = _securityService.HashPassword(userDTO.Password);
                var userEntity = _mapper.Map<User>(userDTO);
                userEntity.UserID = newUserId;
                userEntity.PersonID = newPersonId;
                userEntity.Password = hashedPassword;
                userEntity.TenantID = newTenantId;
                userEntity.RoleID = 1; // Assuming 1 is the RoleID for Admin
                userEntity.IsActive = true;
                userEntity.CreatedAt = DateTime.UtcNow;
                userEntity.IsBlocked = false;


                await _unitOfWork.TenantRepository.CreateTenantAsync(tenantEntity);
                await _unitOfWork.PersonRepository.AddNewPersonAsync(personEntity);
                await _unitOfWork.UserRepository.AddNewUserAsync(userEntity);

                await _unitOfWork.CommitAsync();

                LoginResult login = await LoginAsync(userDTO.Username, userDTO.Password);
                
                if(login.status == LoginStatus.Success)
                return OperationResult<LoginResult>.Success(login);
                return OperationResult<LoginResult>.Failure(OperationStatus.loginFailed, "Admin registration succeeded but login failed, please try to login with your credentials");
            }
            catch(Exception ex)

            {
                await _unitOfWork.RollbackAsync();

                throw;
            }

           

        }

    }

    }

