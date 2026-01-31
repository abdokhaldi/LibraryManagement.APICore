using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;



namespace LibraryManagement.BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
       

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _securityService = securityService;
        }

       public async Task<PagedList<UserForDisplayDTO>> GetActiveUsersAsync(UserParameters parameters)
        {
            var pagedUsers = await _unitOfWork.UserRepository.GetActiveUsersAsync(parameters);

            var usersDTO = _mapper.Map<List<UserForDisplayDTO>>(pagedUsers.Items);

            return pagedUsers.MapTo(usersDTO);
        }

        public async Task<OperationResult<int>> RegisterUserAsync(UserForCreationDTO userDTO, string creator)

        {
            var role = _unitOfWork.RoleRepository.GetRoleForReadOnlyAsync(userDTO.RoleID);
            if (role == null )
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "The role not existing");

            }

           if (creator != "Admin" && creator != "Librarian")
           {
               return OperationResult<int>.Failure(OperationStatus.Forbidden, "You are not allowed  to create users accounts.");
          
           }
          
           if (creator == "Librarian" && userDTO.RoleName != "Member")
           {
               return OperationResult<int>.Failure(
                   OperationStatus.Forbidden,
                   "Access Denied: Librarians are only authorized to create Member accounts."
                   );
            }

            
            var personStatus = await _unitOfWork.PersonRepository.CheckPersonStatus(userDTO.PersonID);
            if (personStatus.isNotFound)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, $"The person with ID: {userDTO.PersonID} is not exists .");
            }
            if (personStatus.isNotActive)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "Cannot create a user for an inactive person .");

            }

            var personAsUser = await _unitOfWork.UserRepository.GetUserAsPersonAsync(userDTO.PersonID);
            if (personAsUser!= null)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict,$"The person with ID:{personAsUser.PersonID} is already created as a user .");
            }
            bool isUserExists = await _unitOfWork.UserRepository. IsUsernameExistsAsync(userDTO.Username);
            if (isUserExists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "This username is already Used , try another one ."); 
            }
            

            var userEntity = _mapper.Map<User>(userDTO);
            string passwordHashed = _securityService.HashPassword(userEntity.Password);
            userEntity.Password = passwordHashed;
            userEntity.CreatedAt = DateTime.UtcNow;
            userEntity.IsBlocked = false;
            userEntity.IsActive = true;
            
        await _unitOfWork.UserRepository.AddNewUserAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<int>.Success(userEntity.UserID);
        }
        public async Task<OperationResult<UserForDisplayDTO>> GetUserDetailsAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserForReadOnlyAsync(id);
            if (user == null)
            {
                return OperationResult<UserForDisplayDTO>.Failure(OperationStatus.NotFound, $"The user with ID: {id} is not found");
            }
            var userDTO = _mapper.Map<UserForDisplayDTO>(user);
            
            return OperationResult<UserForDisplayDTO>.Success(userDTO);
        }
        public async Task<OperationResult> UpdateUserAsync(int id, UserForUpdateDTO userDTO)
        {
            
            var user = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (user ==null )
            {
                    return OperationResult.Failure(OperationStatus.NotFound, $"The user with ID:{id} was not found for update.");
            }

            if (userDTO.PersonID.HasValue && userDTO.PersonID.Value!= user.PersonID)
            {
                var personStatus = await _unitOfWork.PersonRepository.CheckPersonStatus(userDTO.PersonID.Value);
                if (personStatus.isNotFound)
                {
                    return OperationResult.Failure(OperationStatus.Conflict, $"The person with ID: {userDTO.PersonID} is not exists .");
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
                    return OperationResult<int>.Failure(OperationStatus.Conflict, $"The person with ID:{personAsUser.PersonID} is already is another user in the system .");
          
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
        public async Task<OperationResult> DeactivateUserAsync(int id){
            var userForDeactivate = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForDeactivate==null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"User with ID:{id} not found .");
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
        public async Task<OperationResult> ActivateUserAsync(int id){
            var userForActivate = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForActivate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"user with ID:{id} not found .");
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
        public async Task<OperationResult> BlockUserAsync(int id){
            var userForBlock = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForBlock==null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"User with ID:{id} not found .");
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
        
        public async Task<OperationResult> UnblockUserAsync(int id) {
            var userForUnBlock = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (userForUnBlock == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"User with ID:{id} not found .");
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

