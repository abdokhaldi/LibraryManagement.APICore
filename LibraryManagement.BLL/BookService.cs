
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.BookDTOs;
using System.Data;
using LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace LibraryManagement.BLL
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
       public BookService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       
       public async Task<int?> CreateNewBookAsync(BookForCreationDTO bookDTO)
        {
            
              var exists = await _unitOfWork.BookRepository.IsTitleExistsAsync(bookDTO.Title);
              if (exists)
              {
                return null;
              }

            var bookToCreate = _mapper.Map<Book>(bookDTO);
            bookToCreate.IsActive = true;
            await _unitOfWork.BookRepository.AddNewBookAsync(bookToCreate);
                await _unitOfWork.SaveChangesAsync();
           
            return bookToCreate.BookID;
        }
       public async Task<int> UpdateBookAsync(int id,BookForUpdateDTO bookDTO)
        {
             var bookToUpdate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(id);

             if (bookToUpdate == null)
             {
                return 0;
             }
            if (bookDTO.Title != null && bookDTO.Title !=bookToUpdate.Title)
            {
                bool titleExists = await _unitOfWork.BookRepository.IsTitleExistsAsync(bookDTO.Title);
                if (titleExists == true)
                {
                    return -1;
                }
            }
            _mapper.Map(bookDTO, bookToUpdate);
            
            await _unitOfWork.SaveChangesAsync();

            return 1;
          }

       public async Task<bool> ActivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                return false;
            }
            if (bookToActivate.IsActive)
            {
                return true;
            }
            
            bookToActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
       public async Task<bool> DeactivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                return false;
            }
            if (!bookToActivate.IsActive)
            {
                return true;
            }

            bookToActivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
       public async Task<List<BookForDisplayDTO>> GetAllActiveBooksAsync()
        {
            var booksQuery = await _unitOfWork.BookRepository.GetQueryableBooksAsync();

            var activeBooksDTO = await booksQuery
                .Where(b => b.IsActive == true)
                .ProjectTo<BookForDisplayDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return activeBooksDTO;
        }
       public async Task<BookForDisplayDTO?> GetBookDetailsAsync (int bookID) 
        {
            var bookEntity = await _unitOfWork.BookRepository.GetBookForReadOnlyAsync(bookID);
            if (bookEntity == null)
            {
                return null;
            }
            var bookDTO = _mapper.Map<BookForDisplayDTO>(bookEntity);
            return bookDTO;
        }  




    }
}
