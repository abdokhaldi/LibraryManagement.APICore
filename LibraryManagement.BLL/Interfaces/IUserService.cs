using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserForDisplayDTO>> GetActiveUsersAsync(UserParameters parameters);
        Task<OperationResult<Guid>> RegisterUserAsync(UserForCreationDTO userDTO , string creator);
        Task<OperationResult<UserForDisplayDTO>> GetUserDetailsAsync(Guid id);
        Task<OperationResult> UpdateUserAsync(string creator,Guid id, UserForUpdateDTO userDTO);
        Task<OperationResult> DeactivateUserAsync(Guid id);
        Task<OperationResult> ActivateUserAsync(Guid id);
        Task<OperationResult> BlockUserAsync(Guid id);
        Task<OperationResult> UnblockUserAsync(Guid id);

    }
}
