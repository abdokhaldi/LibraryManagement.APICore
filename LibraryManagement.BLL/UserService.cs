using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;



namespace LibraryManagement.BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
       

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _securityService = securityService;
            _httpContextAccessor = httpContextAccessor;
        }

       public async Task<PagedList<UserForDisplayDTO>> GetActiveUsersAsync(UserParameters parameters)
        {
            var pagedUsers = await _unitOfWork.UserRepository.GetActiveUsersAsync(parameters);

            var usersDTO = _mapper.Map<List<UserForDisplayDTO>>(pagedUsers.Items);

            return pagedUsers.MapTo(usersDTO);
            
        }

    
   public async Task<OperationResult<Guid>> RegisterUserAsync(UserForCreationDTO userDTO,string creator)
        {
            var role = await _unitOfWork.RoleRepository.GetRoleForReadOnlyAsync(userDTO.RoleID);
            
            if (role == null)
            {
                return OperationResult<Guid>.Failure(OperationStatus.Conflict, "The role does not exist");
            }

            if (string.IsNullOrEmpty(creator))
            {
                return OperationResult<Guid>.Failure(OperationStatus.Forbidden, "You haven't a token with a valid role");

            }
            if (creator == "Member")
            {
                return OperationResult<Guid>.Failure(OperationStatus.Forbidden, "You do not have the necessary permissions to create new accounts.");
            }

            if (creator != "Admin" && creator != "Librarian")
            {
                return OperationResult<Guid>.Failure(OperationStatus.Forbidden, "You are not allowed to create user accounts.");
           
            }
            
           
            if (creator == "Librarian" && role.RoleName != "Member")
            {
                return OperationResult<Guid>.Failure(
                    OperationStatus.Forbidden,
                    "Access Denied: Librarians are only authorized to create Member accounts."
                    );
            }

            // Check if username already exists
            bool isUserExists = await _unitOfWork.UserRepository.IsUsernameExistsAsync(userDTO.Username);
            if (isUserExists)
            {
                return OperationResult<Guid>.Failure(OperationStatus.Conflict, "This username is already used, try another one."); 
            }

            Guid personId;

            // Start transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                
                    // Create new Person + User in transaction
                    // Check if email or phone already exists
                    var emailExists = await _unitOfWork.PersonRepository.IsEmailExists(userDTO.Person!.Email);
                    if (emailExists)
                    {
                        await _unitOfWork.RollbackAsync();
                        return OperationResult<Guid>.Failure(OperationStatus.Conflict, "Email already exists.");
                    }
                    var phoneExists = await _unitOfWork.PersonRepository.IsPhoneExists(userDTO.Person.Phone);
                    if (phoneExists)
                    {
                        await _unitOfWork.RollbackAsync();
                        return OperationResult<Guid>.Failure(OperationStatus.Conflict, "Phone number already exists.");
                    }

                    var personEntity = _mapper.Map<Person>(userDTO.Person);
                    personEntity.PersonID = Guid.NewGuid();
                    personEntity.IsActive = true;
                    
                    await _unitOfWork.PersonRepository.AddNewPersonAsync(personEntity);
                    personId = personEntity.PersonID;
                
                
                // Create User
                var userEntity = _mapper.Map<User>(userDTO);
                string passwordHashed = _securityService.HashPassword(userEntity.Password);
                userEntity.UserID = Guid.NewGuid();
                userEntity.Password = passwordHashed;
                userEntity.CreatedAt = DateTime.UtcNow;
                userEntity.IsBlocked = false;
                userEntity.IsActive = true;
                userEntity.PersonID = personId;
                
                
                await _unitOfWork.UserRepository.AddNewUserAsync(userEntity);
               
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return OperationResult<Guid>.Success(userEntity.UserID);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<OperationResult<UserForDisplayDTO>> GetUserDetailsAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetUserForReadOnlyAsync(id);
            if (user == null)
            {
                return OperationResult<UserForDisplayDTO>.Failure(OperationStatus.NotFound, $"The user with SettingsID: {id} is not found");
            }
            var userDTO = _mapper.Map<UserForDisplayDTO>(user);
            
            return OperationResult<UserForDisplayDTO>.Success(userDTO);
        }
        public async Task<OperationResult> UpdateUserAsync(Guid id, UserForUpdateDTO userDTO)
        {
            
            var user = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (user ==null )
            {
                    return OperationResult.Failure(OperationStatus.NotFound, $"The user with SettingsID:{id} was not found for update.");
            }

            if (userDTO.PersonID.HasValue && userDTO.PersonID.Value != user.PersonID)
            {
                var personStatus = await _unitOfWork.PersonRepository.CheckPersonStatus(userDTO.PersonID.Value);
                if (personStatus.isNotFound)
                {
                    return OperationResult.Failure(OperationStatus.Conflict, $"The person with SettingsID: {userDTO.PersonID} is not exists .");
                }
                if (personStatus.isNotActive)
                {
                    return OperationResult.Failure(OperationStatus.Conflict, "Cannot create a user for an inactive person .");

                }
            }

            if (userDTO.PersonID.HasValue)
            {
                var personAsUser = await _unitOfWork.UserRepository.GetUserAsPersonAsync(userDTO.PersonID.Value);
                if (personAsUser != null && personAsUser.PersonID != user.PersonID)
                {
                    return OperationResult<int>.Failure(OperationStatus.Conflict, $"The person with SettingsID:{personAsUser.PersonID} is already is another user in the system .");
          
                }
            }
            if (!string.IsNullOrEmpty(userDTO.Username)) 
                {
                bool isUsedUsername = await _unitOfWork.UserRepository.IsUsernameExistsForUpdateAsync(user.UserID, userDTO.Username);
                if (isUsedUsername)
                {
                    return OperationResult.Failure(OperationStatus.Conflict, $"The username: {userDTO.Username} is already taken by another user.");
                }
              }
            _mapper.Map(userDTO, user);

            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
        // //
        public async Task<OperationResult> DeactivateUserAsync(Guid id){
            var userForDeactivate = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForDeactivate==null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"AdminUser with SettingsID:{id} not found .");
            }
            if (userForDeactivate.IsBlocked)
            {
                return OperationResult.Failure(OperationStatus.Blocked,"cannot deactivate blocked user");
            }

            if (!userForDeactivate.IsActive)
            {
                return OperationResult.Success() ;
            }
            userForDeactivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
        public async Task<OperationResult> ActivateUserAsync(Guid id){
            var userForActivate = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForActivate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"user with SettingsID:{id} not found .");
            }
            if (userForActivate.IsBlocked)
            {
                return OperationResult.Failure(OperationStatus.Blocked,"cannot activate blocked user .");
            }

            if (userForActivate.IsActive)
            {
                return OperationResult.Success();
            }
            userForActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();

        }
        public async Task<OperationResult> BlockUserAsync(Guid id){
            var userForBlock = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForBlock==null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"AdminUser with SettingsID:{id} not found .");
            }
            if (userForBlock.IsBlocked && !userForBlock.IsActive)
            {
                return OperationResult.Success();
            } 
                userForBlock.IsBlocked = true;
                userForBlock.IsActive = false;
                await _unitOfWork.SaveChangesAsync();
                return OperationResult.Success();
            }
        
        public async Task<OperationResult> UnblockUserAsync(Guid id) {
            var userForUnBlock = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForUnBlock == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"AdminUser with SettingsID:{id} not found .");
            }
            if (!userForUnBlock.IsBlocked)
            {
                return OperationResult.Success();
            }
            userForUnBlock.IsBlocked = false;
            
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();

        }
     }

}

