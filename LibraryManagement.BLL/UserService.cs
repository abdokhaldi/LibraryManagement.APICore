using System;
using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.DTO.OperationResult;
using LibraryManagement.DTO.Common;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;



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

       public async Task<List<UserForDisplayDTO>> GetAllActiveUsersAsync()
        {
            var usersQuery = await _unitOfWork.UserRepository.GetQueryableUsersAsync();
            var users = await usersQuery
                .Where(u => u.IsActive == true)
                .ProjectTo<UserForDisplayDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<OperationResult<int>> RegisterUserAsync(UserForCreationDTO userDTO)
        {
            bool isUserExists = await _unitOfWork.UserRepository. IsUsernameExistsAsync(userDTO.Username);
            if (isUserExists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "This username is already Used , try another one ."); 
            }
            bool isPersonActive = await _unitOfWork.PersonRepository.IsPersonActive(userDTO.PersonID);
            if (!isPersonActive)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict,"Cannot create user for an inactive person .");
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
            var userWithSameName = await _unitOfWork.UserRepository.GetUserByUsernameAsync(user.Username);
            if (userWithSameName != null && userWithSameName.UserID != id)
            {
                return OperationResult.Failure(OperationStatus.Conflict, $"The username: {userDTO.Username} is already taken by another user.");
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

