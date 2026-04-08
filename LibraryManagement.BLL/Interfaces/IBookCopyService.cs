

using LibraryManagement.DTO.BookCopyDTO;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBookCopyService
    {
        Task<OperationResult<int>> CreateCopyAsync(BookCopyForCreationDTO bookCopyDTO);
        Task<PagedList<BookCopyForDisplayDTO>> GetCopiesAsync(BookCopyParameters parameters);
        Task<OperationResult> UpdateCopyAsync(int copyID ,BookCopyForUpdateDTO bookCopyDTO);
        Task<OperationResult> ActivateCopyAsync(int copyID);
        Task<OperationResult> DeactivateCopyAsync(int copyID);
        Task<OperationResult<BookCopyForDisplayDTO>> GetBookCopyAsync(int copyID);
        Task<OperationResult<BookCopyForDisplayDTO>> GetBookCopyAsync(string barcode);

    }
}
