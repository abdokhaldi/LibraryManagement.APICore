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
        Task<List<BookForDisplayDTO>> GetAllBooksInfoAsync();

        Task<BookForDisplayDTO?> GetBookDetailsAsync(int bookID);

        Task<int> CreateNewBookAsync(BookForCreationDTO bookDTO)

        Task UpdateBookAsync(BookForUpdateDTO bookDTO);
        Task ActivateBookAsync(int bookID);
        Task DeactivateBookAsync(int bookID);

    }
}
