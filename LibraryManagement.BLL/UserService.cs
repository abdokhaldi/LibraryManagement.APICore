using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> RegisterUserAsync(UserForCreationDTO userDTO)
        {
            bool isUserExists = await _unitOfWork.UserRepository. IsUsernameExistsAsync(userDTO.Username);
            if (isUserExists)
            {
                return -1;
            }
            var userEntity = _mapper.Map<User>(userDTO);
            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);
            userEntity.Password = passwordHashed;
            userEntity.CreatedAt = DateTime.UtcNow;
            userEntity.IsBlocked = false;
            userEntity.IsActive = true;
            
        await _unitOfWork.UserRepository.AddNewUserAsync(userEntity);
            await _unitOfWork.SaveChangesAsync();

            return userEntity.UserID;
        }
        public async Task<UserForDisplayDTO?> GetUserDerailsAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserForReadOnlyAsync(id);
            if (user == null)
            {
                return null;
            }
            var userDTO = _mapper.Map<UserForDisplayDTO>(user);
            
            return userDTO;
        }
        public async Task<(bool success,string error)> UpdateUserAsync(int id, UserForUpdateDTO userDTO)
        {
            var user = await _unitOfWork.UserRepository.GetUserForUpdateAsync(id);
            if (user ==null )
            {
                    return (false, $"The user with ID:{id} was not found for update.");
            }
            var userWithSameName = await _unitOfWork.UserRepository.GetUserByUsernameAsync(user.Username);
            if (userWithSameName != null && userWithSameName.UserID != id)
            {
                return (false, $"The username: {userDTO.Username} is already taken by another user.");
            }
            _mapper.Map(userDTO, user);

            await _unitOfWork.SaveChangesAsync();
            return (true,string.Empty);
        }
        
    }

}

