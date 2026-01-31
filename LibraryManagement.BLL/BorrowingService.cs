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
            var bookEntity = await _unitOfWork.BookRepository.GetBookForUpdateAsync(borrowingDTO.BookID);

            if (bookEntity == null)
            {
                return OperationResult<int>.Failure(OperationStatus.NotFound, $"The book with ID:{borrowingDTO.BookID} was not found for borrowing");
            }
            if (bookEntity.Quantity <= 0)
            {
             return OperationResult<int>.Failure(OperationStatus.Conflict, $"cannot borrow this book , the book quantity is 0");

            }
            var memberToCreate = new MemberForCreationDTO { PersonID = borrowingDTO.PersonID, JoinDate = DateTime.Now, IsActive = true };

            var memberEntity = await _memberService.CreateMemberAsync(memberToCreate);

            bookEntity.Quantity--;

            var borrowingEntity = _mapper.Map<Borrowing>(borrowingDTO);

            borrowingEntity.Member = memberEntity;
            borrowingEntity.BorrowingDate = DateTime.Now;
            borrowingEntity.Status = "Borrowed";
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
            var bookEntity = await _unitOfWork.BookRepository.GetBookForUpdateAsync(borrowingEntity.BookID);
            if (bookEntity == null)
            {
                return OperationResult.Failure(OperationStatus.Conflict , "Associated book is missing from the system .");
            }
            bookEntity.Quantity++;

            borrowingEntity.ReturnDate = DateTime.Now;
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

       // public async Task<List<BorrowingForDisplayDTO>> GetOverdueAsync()
       // {
         //   var borrowingsQuery = await _unitOfWork.BorrowingRepository.GetBorrowingsAsync();
         //
         //   var borrowingsDTO = await borrowingsQuery
         //       .Where(b => b.ReturnDate == null && b.DueDate < DateTime.UtcNow)
         //       .ProjectTo<BorrowingForDisplayDTO>(_mapper.ConfigurationProvider)
         //       .ToListAsync();
         //
         //   return borrowingsDTO;
       // }
    }
}
