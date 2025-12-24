using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(UserForCreationDTO userDTO);
        Task<UserForDisplayDTO?> GetUserDerailsAsync(int id);
        Task<(bool success, string error)> UpdateUserAsync(int id, UserForUpdateDTO userDTO);
    }
}
