
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.BookDTOs;
using System.Data;
using LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.DTO.OperationResult;
using LibraryManagement.DTO.Common;


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
       
       public async Task<OperationResult<int>> CreateNewBookAsync(BookForCreationDTO bookDTO)
        {
            
              var exists = await _unitOfWork.BookRepository.IsTitleExistsAsync(bookDTO.Title);
              if (exists)
              {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "This title is already exists , books must have a unique title.");
              }

            var bookToCreate = _mapper.Map<Book>(bookDTO);
            bookToCreate.IsActive = true;
            await _unitOfWork.BookRepository.AddNewBookAsync(bookToCreate);
                await _unitOfWork.SaveChangesAsync();
           
            return OperationResult<int>.Success(bookToCreate.BookID);
        }
       public async Task<OperationResult> UpdateBookAsync(int id,BookForUpdateDTO bookDTO)
        {
             var bookToUpdate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(id);

             if (bookToUpdate == null)
             {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book with ID:{id} was not found for update .");
             }
            if (bookDTO.Title != null && bookDTO.Title !=bookToUpdate.Title)
            {
                bool titleExists = await _unitOfWork.BookRepository.IsTitleExistsAsync(bookDTO.Title);
                if (titleExists == true)
                {
                    return OperationResult.Failure(OperationStatus.Conflict, $"The title is already exists .");

                }
            }
            _mapper.Map(bookDTO, bookToUpdate);
            
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
          }

       public async Task<OperationResult> ActivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"The book with ID:{bookID} not found for activate");
            }
            if (bookToActivate.IsActive)
            {
                return OperationResult.Success();

            }

            bookToActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
       public async Task<OperationResult> DeactivateBookAsync(int bookID)
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book with ID:{bookID} not found for deactivate");

            }
            if (!bookToActivate.IsActive)
            {
                return OperationResult.Success();
            }

            bookToActivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
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
       public async Task<OperationResult<BookForDisplayDTO>> GetBookDetailsAsync (int bookID) 
        {
            var bookEntity = await _unitOfWork.BookRepository.GetBookForReadOnlyAsync(bookID);
            if (bookEntity == null)
            {
                return OperationResult<BookForDisplayDTO>.Failure(OperationStatus.NotFound, $"The book with ID:{bookID} not found ");

            }
            var bookDTO = _mapper.Map<BookForDisplayDTO>(bookEntity);
            return OperationResult<BookForDisplayDTO>.Success(bookDTO);
        }  




    }
}
