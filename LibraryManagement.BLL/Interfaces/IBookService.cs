
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBookService
    {
        Task<PagedList<BookForDisplayDTO>> GetActiveBooksAsync(BookParameters parameters);

        Task<OperationResult<BookForDisplayDTO>> GetBookDetailsAsync(int bookID);

        Task<OperationResult<int>> CreateNewBookAsync(BookForCreationDTO bookDTO);

        Task<OperationResult> UpdateBookAsync(int id,BookForUpdateDTO bookDTO);
        Task<OperationResult> ActivateBookAsync(int bookID);
        Task<OperationResult> DeactivateBookAsync(int bookID);

    }
}
