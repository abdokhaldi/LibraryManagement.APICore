
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.Domain.Entities;
using AutoMapper;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.Common;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;


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
            string savedImagePath = "/images/covers/default.jpg";
            string uniqueFileName = "";

            if (bookDTO.Image != null && bookDTO.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", "covers" );
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string extension = Path.GetExtension(bookDTO.Image.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + extension;

                string fullPhysicalPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(fullPhysicalPath, FileMode.Create))
                {
                    await bookDTO.Image.CopyToAsync(fileStream);
                }

                savedImagePath = $"images/covers/{uniqueFileName}";
            }

            var bookToCreate = _mapper.Map<Book>(bookDTO);
            bookToCreate.IsActive = true;
            bookToCreate.ImagePath = uniqueFileName;

            await _unitOfWork.BookRepository.AddNewBookAsync(bookToCreate);
                await _unitOfWork.SaveChangesAsync();
           
            return OperationResult<int>.Success(bookToCreate.BookID);
        }

       public async Task<OperationResult> UpdateBookAsync(int id,BookForUpdateDTO bookDTO, string webRootPath)
        {
             var bookToUpdate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(id);

           
             if (bookToUpdate == null)
             {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book with SettingsID:{id} was not found for update .");
             }

            string savedImagePath = "images/covers/default.jpg";
            string newFileName = "";

            if (bookDTO.Image != null)
            {
                if (!string.IsNullOrEmpty(bookToUpdate.ImagePath))
                {
                    string oldFilePath = Path.Combine(webRootPath,"images","covers",bookToUpdate.ImagePath);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(bookDTO.Image.FileName);
                string uploadsFolder = Path.Combine(webRootPath, "images", "covers");
               
               
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                savedImagePath = Path.Combine(uploadsFolder,newFileName);
                using (var stream = new FileStream(savedImagePath, FileMode.Create))
                {
                   await bookDTO.Image.CopyToAsync(stream);
                }
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
            bookToUpdate.ImagePath = newFileName ;


            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
          }

       public async Task<OperationResult> ActivateBookAsync(int bookID) 
        {
            var bookToActivate = await _unitOfWork.BookRepository.GetBookForUpdateAsync(bookID);
            if (bookToActivate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"The book with SettingsID:{bookID} not found for activate");
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
                return OperationResult.Failure(OperationStatus.NotFound, $"The book with SettingsID:{bookID} not found for deactivate");

            }
            if (!bookToActivate.IsActive)
            {
                return OperationResult.Success();
            }

            bookToActivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        } 

       public async Task<PagedList<BookForDisplayDTO>> GetActiveBooksAsync(BookParameters parameters)
        {
            var pagedBooks = await _unitOfWork.BookRepository.GetActiveBooksAsync(parameters);

            var booksDTO = _mapper.Map<List<BookForDisplayDTO>>(pagedBooks.Items);

            return  pagedBooks.MapTo(booksDTO);
            
        }

       public async Task<OperationResult<BookForDisplayDTO>> GetBookDetailsAsync (int bookID) 
        {
            var bookEntity = await _unitOfWork.BookRepository.GetBookForReadOnlyAsync(bookID);
            if (bookEntity == null)
            {
                return OperationResult<BookForDisplayDTO>.Failure(OperationStatus.NotFound, $"The book with bookID:{bookID} not found ");

            }
            var bookDTO = _mapper.Map<BookForDisplayDTO>(bookEntity);
            return OperationResult<BookForDisplayDTO>.Success(bookDTO);
        }  




    }
}
