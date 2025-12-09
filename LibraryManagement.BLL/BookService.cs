
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DTO.BookDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Entities;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.BLL
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
       public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
       public async Task<int> CreateNewBookAsync(BookForCreationDTO bookDTO)
        {
            
                var exists = await _unitOfWork.BookRepository.IsTitleExistsAsync(bookDTO.Title);
                if (exists)
                {
                    throw new ArgumentException("This title is already exists .");
                }

                var bookToCreate = new Book
                {
                    Title = bookDTO.Title,
                    Author = bookDTO.Author,
                    Publisher = bookDTO.Publisher,
                    YearPublished = bookDTO.YearPublished,
                    CategoryID = bookDTO.CategoryID,
                    Quantity = bookDTO.Quantity,
                    ImagePath = bookDTO.ImagePath,
                    IsActive = true
                };
               
                await _unitOfWork.BookRepository.AddNewBookAsync(bookToCreate);
                await _unitOfWork.SaveChangesAsync();
           
            return bookToCreate.BookID;
        }
       public async Task UpdateBookAsync(BookForUpdateDTO bookDTO)
        {
              var bookToUpdate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookDTO.BookID);

             if (bookToUpdate == null)
             {
                 throw new ArgumentNullException("The book cannot be null");
             }

            bookToUpdate.Title = bookDTO.Title;
            bookToUpdate.Author = bookDTO.Author;
            bookToUpdate.Publisher = bookDTO.Publisher;
            bookToUpdate.YearPublished = bookDTO.YearPublished;
            bookToUpdate.Quantity = bookDTO.Quantity;
            bookToUpdate.CategoryID = bookDTO.CategoryID;
            bookToUpdate.IsActive = bookDTO.IsActive;
           
            await _unitOfWork.SaveChangesAsync();
          }
       public async Task ActivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                throw new KeyNotFoundException($"The book not found with Id:{bookID}");
            }
            if (bookToActivate.IsActive)
            {
                return;
            }
            
            bookToActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
         }
       public async Task DeactivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                throw new KeyNotFoundException($"The book not found with Id:{bookID}");
            }
            if (!bookToActivate.IsActive)
            {
                return;
            }

            bookToActivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
        }
       public async Task<List<BookForDisplayDTO>> GetAllActiveBooksAsync()
        {
            var booksQuery = await _unitOfWork.BookRepository.GetQueryableBooksAsync();
                booksQuery.Where(b => b.IsActive == true);
            await booksQuery.ToListAsync();

            

            var listDTO = await booksQuery .Select(
                b => new BookForDisplayDTO
                {
                    Title = b.Title,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    YearPublished = b.YearPublished,
                    CategoryName = b.Category.CategoryName,
                    Quantity = b.Quantity,
                    IsActive = b.IsActive,
                }
                ).ToListAsync();
            return listDTO;
        }





    }
}
