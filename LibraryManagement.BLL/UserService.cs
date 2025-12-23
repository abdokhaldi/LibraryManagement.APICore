using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
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
    }

    }

