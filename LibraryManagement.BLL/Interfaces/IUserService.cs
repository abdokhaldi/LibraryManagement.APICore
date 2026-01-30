using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserForDisplayDTO>> GetActiveUsersAsync(UserParameters parameters);
        Task<OperationResult<int>> RegisterUserAsync(UserForCreationDTO userDTO , string creator);
        Task<OperationResult<UserForDisplayDTO>> GetUserDetailsAsync(int id);
        Task<OperationResult> UpdateUserAsync(int id, UserForUpdateDTO userDTO);
        Task<OperationResult> DeactivateUserAsync(int id);
        Task<OperationResult> ActivateUserAsync(int id);
        Task<OperationResult> BlockUserAsync(int id);
        Task<OperationResult> UnblockUserAsync(int id);

    }
}
