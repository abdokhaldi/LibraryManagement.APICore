using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IBorrowingService
    {
        Task<OperationResult<int>> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO);
        Task<OperationResult<BorrowingForDisplayDTO>> GetBorrowingDetailsAsync(int id);
        Task<OperationResult> ReturnBookAsync(int id);
        Task<OperationResult> ExtendDueDateAsync(int id, BorrowingForExtendDTO borrowingDTO);
        Task<List<BorrowingForDisplayDTO>> GetBorrowingsAsync();
        Task<List<BorrowingForDisplayDTO>> GetOverdueAsync();

    }
}
