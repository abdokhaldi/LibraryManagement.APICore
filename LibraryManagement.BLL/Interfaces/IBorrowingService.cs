using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IBorrowingService
    {
        Task<OperationResult<int>> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO);
        Task<OperationResult<BorrowingForDisplayDTO>> GetBorrowingDetailsAsync(int id);
        Task<OperationResult> ReturnBookAsync(int id);
        Task<OperationResult> ExtendDueDateAsync(int id, BorrowingForExtendDTO borrowingDTO);
        Task<PagedList<BorrowingForDisplayDTO>> GetBorrowingsAsync(BorrowingParameters parameters);
      //  Task<List<BorrowingForDisplayDTO>> GetOverdueAsync();

    }
}
