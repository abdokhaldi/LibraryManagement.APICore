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
        private readonly BarcodeGeneratorService _barcodeGeneratorService;
        
        public BookCopyService(IUnitOfWork unitOfWork, IMapper mapper, BarcodeGeneratorService barcodeGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _barcodeGeneratorService = barcodeGeneratorService;
        }

      public async  Task<OperationResult<BookCopyForDisplayDTO>> CreateCopyAsync(BookCopyForCreationDTO bookCopyDTO) 
        {
            int quantity = bookCopyDTO.Quantity > 0 ? bookCopyDTO.Quantity : 1;
            BookCopyForDisplayDTO firstCopyDTO = null;

            for (int i = 0; i < quantity; i++)
            {
                // Auto-generate barcode for each copy
                string barcode = _barcodeGeneratorService.GenerateShortBarcode();
                
                // Check if barcode already exists and regenerate if needed (max 3 attempts)
                var existingCopy = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(cb => cb.Barcode == barcode);
                int attempts = 0;
                while (existingCopy != null && attempts < 3)
                {
                    barcode = _barcodeGeneratorService.GenerateShortBarcode();
                    existingCopy = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(cb => cb.Barcode == barcode);
                    attempts++;
                }

                if (existingCopy != null)
                {
                    return OperationResult<BookCopyForDisplayDTO>.Failure(OperationStatus.Conflict, $"Failed to generate unique barcode after 3 attempts for copy {i + 1}");
                }

                var copyToCreate = _mapper.Map<BookCopy>(bookCopyDTO);
                copyToCreate.Barcode = barcode;
                copyToCreate.IsActive = true;

                await _unitOfWork.BookCopyRepository.AddNewBookCopyAsync(copyToCreate);
                
                // Save after each copy to ensure barcode uniqueness checks work properly
                await _unitOfWork.SaveChangesAsync();

                // Return the first created copy with full details
                if (i == 0)
                {
                    firstCopyDTO = _mapper.Map<BookCopyForDisplayDTO>(copyToCreate);
                }
            }

            return OperationResult<BookCopyForDisplayDTO>.Success(firstCopyDTO!);
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
        public async Task<OperationResult<BookCopyForDisplayDTO>> GetBookCopyAsync(string barcode)
        {
            var copyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(bc => bc.Barcode == barcode);

            if (copyEntity == null)
            {
                return OperationResult<BookCopyForDisplayDTO>.Failure(OperationStatus.NotFound, $"The book copy with id : {barcode} was not found .");
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
