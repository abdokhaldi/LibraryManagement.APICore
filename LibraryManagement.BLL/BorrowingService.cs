using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Types;

namespace LibraryManagement.BLL
{  
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        public BorrowingService(IUnitOfWork unitOfWork, IMemberService memberService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memberService = memberService;
        }

        public async Task<OperationResult<int>> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO)
        {
            var bookCopyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(cb => cb.BookCopyID==borrowingDTO.BookCopyID ,trackChanges:true);

            if (bookCopyEntity == null)
            {
                return OperationResult<int>.Failure(OperationStatus.NotFound, $"The book with ID:{borrowingDTO.BookCopyID} was not found for borrowing");
            }
            int availableQuantity = await _unitOfWork.BookCopyRepository.GetAvailableBookCopiesQuantityAsync(bookCopyEntity.BookID);
            if (availableQuantity <= 0)
            {
             return OperationResult<int>.Failure(OperationStatus.Conflict, $"cannot borrow this book , the book quantity is 0");

            }
            var memberToCreate = new MemberForCreationDTO { PersonID = borrowingDTO.PersonID, JoinDate = DateTime.Now, IsActive = true };

            var memberEntity = await _memberService.CreateMemberAsync(memberToCreate);

            bookCopyEntity.Status = CopyStatus.Borrowed;

            var borrowingEntity = _mapper.Map<Borrowing>(borrowingDTO);

            borrowingEntity.Member = memberEntity;
            borrowingEntity.BorrowingDate = DateTime.UtcNow;
            borrowingEntity.Status = "Borrowed";
            borrowingEntity.InitialFees = 15.0m; // default value while i create settings
            borrowingEntity.ReturnDate = null;
            borrowingEntity.IsCanceled = false;

            await _unitOfWork.BorrowingRepository.RecordNewBorrowingAsync(borrowingEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return OperationResult<int>.Success(borrowingEntity.BorrowingID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult<BorrowingForDisplayDTO>> GetBorrowingDetailsAsync(int id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetBorrowingForReadOnlyAsync(id);
            if (borrowing == null)
            {
                return OperationResult<BorrowingForDisplayDTO>.Failure(OperationStatus.NotFound, $"The borrowing with ID:{borrowing?.BorrowingID} was not found .");
            }
            if (borrowing.IsCanceled)
            {
                return OperationResult<BorrowingForDisplayDTO>.Failure(OperationStatus.NotFound, $"The borrowing with ID:{borrowing.BorrowingID} was cancelled .");
            }

            var borrowingDTO = _mapper.Map<BorrowingForDisplayDTO>(borrowing);
            return OperationResult<BorrowingForDisplayDTO>.Success(borrowingDTO);
        }

        public async Task<OperationResult> ReturnBookAsync(int id)
        {
          
            var borrowingEntity = await _unitOfWork.BorrowingRepository.GetBorrowingForUpdateAsync(id);
            
            if (borrowingEntity == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound,$"Borrowing record with ID:{id} was not found .");
            }
            if (borrowingEntity.IsCanceled) {
                return OperationResult.Failure(OperationStatus.Cancelled, "Cannot process return operation for cancelled Borrowing .");
            }

            if (borrowingEntity.ReturnDate != null)
            {
               return OperationResult.Failure(OperationStatus.Conflict,"This book has already been returned .");
            }

            var bookCopyEntity = await _unitOfWork.BookCopyRepository.GetBookCopyAsync(cb => cb.BookCopyID==borrowingEntity.BookCopyID, trackChanges:true);
            if (bookCopyEntity == null)
            {
                return OperationResult.Failure(OperationStatus.Conflict , "Associated book is missing from the system .");
            }
            
            bookCopyEntity.Status = CopyStatus.Available;

            DateTime returnedDate = DateTime.UtcNow;

            var days = (returnedDate - borrowingEntity.DueDate).Days;

            if (days > 0)
            {
                var fineEntity = new Fine
                {
                    BorrowingID = borrowingEntity.BorrowingID,
                    MemberID = borrowingEntity.MemberID,
                    Amount = days * 0.5m, // default value while i create settings value
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    PaidAt = null ,
                    WaiveReason = null
                };

                await _unitOfWork.FineRepository.AddNewFineAsync(fineEntity);
            }

            borrowingEntity.ReturnDate = DateTime.UtcNow;
            
            borrowingEntity.Status = "Returned";

            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

       public async Task<OperationResult> ExtendDueDateAsync(int id, BorrowingForExtendDTO borrowingDTO)
        {
            var borrowingEntity = await _unitOfWork.BorrowingRepository.GetBorrowingForUpdateAsync(id);
            if (borrowingEntity == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"Borrowing record with ID:{id} was not found .");
            }

            if (borrowingEntity.IsCanceled)
            {
                return OperationResult.Failure(OperationStatus.Cancelled, "Cannot extend date for cancelled Borrowing .");
            }

            if (borrowingEntity.ReturnDate != null)
            {
                return OperationResult.Failure(OperationStatus.Conflict, $"The completed borrowing cannot be extended .");

            }
            if (borrowingEntity.DueDate >= borrowingDTO.DueDate)
            {
                return OperationResult.Failure(OperationStatus.ValidationError, $"Invalid date , the new due date must be later than the current due date");
            }
            _mapper.Map(borrowingDTO,borrowingEntity);
           await  _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<PagedList<BorrowingForDisplayDTO>> GetBorrowingsAsync(BorrowingParameters parameters)
        {
            var pagedBorrowings = await _unitOfWork.BorrowingRepository.GetBorrowingsAsync(parameters);

            var pagedDTOs = _mapper.Map<List<BorrowingForDisplayDTO>>(pagedBorrowings.Items);

            return pagedBorrowings.MapTo(pagedDTOs);
        }


       
    }
}
