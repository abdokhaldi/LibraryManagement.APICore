using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.BookCopyDTO;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.DTO.Common;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Types;

namespace LibraryManagement.BLL
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookCopyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      public async  Task<OperationResult<int>> CreateCopyAsync(BookCopyForCreationDTO bookCopyDTO) 
        {
            var copy = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(cb => cb.Barcode==bookCopyDTO.Barcode);

            if (copy != null)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, $"The copy with barcode : {bookCopyDTO.Barcode} is already existing");
            }

            var copyToCreate = _mapper.Map<BookCopy>(bookCopyDTO);
            
            copyToCreate.IsActive = true;

            await _unitOfWork.BookCopyRepository.AddNewBookCopyAsync(copyToCreate);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<int>.Success(copyToCreate.BookCopyID);
        }
      public async  Task<PagedList<BookCopyForDisplayDTO>> GetCopiesAsync(BookCopyParameters parameters)
        {
            var pagedCopies = await _unitOfWork.BookCopyRepository.GetActiveBookCopiesAsync(parameters);
            
            var copiesDTO = _mapper.Map<List<BookCopyForDisplayDTO>>(pagedCopies.Items);

            return pagedCopies.MapTo(copiesDTO);
        }
      public async  Task<OperationResult<BookCopyForDisplayDTO>> GetBookCopyAsync(int copyID) 
        {
            var copyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(c => c.BookCopyID == copyID);
           
            if (copyEntity ==null)
            {
              return  OperationResult<BookCopyForDisplayDTO>.Failure(OperationStatus.NotFound, $"The book copy with id : {copyID} was not found .");
            }

            var copyDTO = _mapper.Map<BookCopyForDisplayDTO>(copyEntity);

            return OperationResult<BookCopyForDisplayDTO>.Success(copyDTO);

        }


        public async  Task<OperationResult> UpdateCopyAsync(int copyID, BookCopyForUpdateDTO bookCopyDTO) 
        {
            var copyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(c => c.BookCopyID==copyID,trackChanges:true);
            if (copyEntity==null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book copy with id : {copyID} was not found for update.");
            }

            _mapper.Map(bookCopyDTO, copyEntity);

            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();

        }

        public async  Task<OperationResult> ActivateCopyAsync(int copyID) 
        {
            var copyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(c => c.BookCopyID == copyID, trackChanges: true);
            if (copyEntity == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book copy with id : {copyID} was not found for update.");
            }
            if (copyEntity.Status == CopyStatus.Lost
                || copyEntity.Status == CopyStatus.Damaged)
            {
                return OperationResult.Failure(OperationStatus.Conflict, $"Cannot activate a lost or damaged copy.");

            }

            if (copyEntity.IsActive)
            {
                return OperationResult.Success();
            }
            copyEntity.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
      public async  Task<OperationResult> DeactivateCopyAsync(int copyID) {
            var copyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(c => c.BookCopyID == copyID, trackChanges: true);
            
            if (copyEntity == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The book copy with id : {copyID} was not found for update.");
            }

            if (copyEntity.Status == CopyStatus.Borrowed)
            {
                return OperationResult.Failure(OperationStatus.Conflict, $"Cannot deactivate a borrowed book copy.");

            }

            if (!copyEntity.IsActive)
            {
                return OperationResult.Success();
            }

            copyEntity.IsActive = false;

            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
        }
    }
}
