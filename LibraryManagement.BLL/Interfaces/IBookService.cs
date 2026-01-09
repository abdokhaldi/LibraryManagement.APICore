
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DTO.OperationResults;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBookService
    {
        Task<List<BookForDisplayDTO>> GetAllActiveBooksAsync();

        Task<OperationResult<BookForDisplayDTO>> GetBookDetailsAsync(int bookID);

        Task<OperationResult<int>> CreateNewBookAsync(BookForCreationDTO bookDTO);

        Task<OperationResult> UpdateBookAsync(int id,BookForUpdateDTO bookDTO);
        Task<OperationResult> ActivateBookAsync(int bookID);
        Task<OperationResult> DeactivateBookAsync(int bookID);

    }
}
