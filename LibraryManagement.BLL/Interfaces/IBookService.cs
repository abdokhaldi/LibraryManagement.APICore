using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.ActivityDTOs;
using LibraryManagement.DTO.BookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBookService
    {
        Task<List<BookForDisplayDTO>> GetAllActiveBooksAsync();

        Task<BookForDisplayDTO?> GetBookDetailsAsync(int bookID);

        Task<int?> CreateNewBookAsync(BookForCreationDTO bookDTO);

        Task<int> UpdateBookAsync(int id,BookForUpdateDTO bookDTO);
        Task<bool> ActivateBookAsync(int bookID);
        Task<bool> DeactivateBookAsync(int bookID);

    }
}
