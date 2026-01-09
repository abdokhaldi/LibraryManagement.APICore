using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserForDisplayDTO>> GetAllActiveUsersAsync();
        Task<OperationResult<int>> RegisterUserAsync(UserForCreationDTO userDTO);
        Task<OperationResult<UserForDisplayDTO>> GetUserDetailsAsync(int id);
        Task<OperationResult> UpdateUserAsync(int id, UserForUpdateDTO userDTO);
        Task<OperationResult> DeactivateUserAsync(int id);
        Task<OperationResult> ActivateUserAsync(int id);
        Task<OperationResult> BlockUserAsync(int id);
        Task<OperationResult> UnblockUserAsync(int id);

    }
}
