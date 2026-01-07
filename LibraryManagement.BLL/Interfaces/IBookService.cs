using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.ActivityDTOs;
using LibraryManagement.DTO.BookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DTO.OperationResult;

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
