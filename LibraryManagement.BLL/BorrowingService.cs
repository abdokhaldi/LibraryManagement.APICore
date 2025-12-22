using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.MemberDTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

        public async Task<int> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO)
        {
            var bookEntity = await _unitOfWork.BookRepository.GetBookForUpdateAsync(borrowingDTO.BookID);

            if (bookEntity == null || bookEntity.Quantity <= 0)
            {
                return 0;
            }
            var memberToCreate = new MemberForCreationDTO { PersonID = borrowingDTO.PersonID, JoinDate = DateTime.Now, IsActive = true };

            var memberEntity = await _memberService.CreateMemberAsync(memberToCreate);

            bookEntity!.Quantity--;

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
                return borrowingEntity.BorrowingID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BorrowingForDisplayDTO?> GetBorrowingDetailsAsync(int id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetBorrowingForReadOnlyAsync(id);
            if (borrowing == null || borrowing!.IsCanceled == true)
            {
                return null;
            }
            var borrowingDTO = _mapper.Map<BorrowingForDisplayDTO>(borrowing);
            return borrowingDTO;
        }
        public async Task<(bool success, string error)> ReturnBookAsync(int id)
        {
          
            var borrowingEntity = await _unitOfWork.BorrowingRepository.GetBorrowingForUpdateAsync(id);
            
            if (borrowingEntity == null || borrowingEntity.IsCanceled==true)
            {
                return (false, "Borrowing record not found or was canceled");
            }
            if (borrowingEntity.ReturnDate != null)
            {
               return  (false,"This book has already been returned .");
            }
            var bookEntity = await _unitOfWork.BookRepository.GetBookForUpdateAsync(borrowingEntity.BookID);
            if (bookEntity == null)
            {
                return(false , "Associated book is missing from the system .");
            }
            bookEntity.Quantity++;

            borrowingEntity.ReturnDate = DateTime.Now;
            borrowingEntity.Status = "Returned";

            await _unitOfWork.SaveChangesAsync();
            return (true, string.Empty);
        }
        public async Task<(bool success,string error)> ExtendDueDateAsync(int id, BorrowingForExtendDTO borrowingDTO)
        {
            var borrowingEntity = await _unitOfWork.BorrowingRepository.GetBorrowingForUpdateAsync(id);
            if (borrowingEntity == null || borrowingEntity.IsCanceled==true)
            {
                return (false , $"The borrowing with ID :{id} is not found or was cancelled .");
            }
            if (borrowingEntity.ReturnDate != null)
            {
                return (false, $"The completed borrowing cannot be updated .");

            }
            if (borrowingEntity.DueDate > borrowingDTO.DueDate)
            {
                return (false,$"Invalid date , the new due date must be later than the current due date");
            }
            _mapper.Map(borrowingDTO,borrowingEntity);
           await  _unitOfWork.SaveChangesAsync();
            return (true,string.Empty);
        }

      public async Task<List<BorrowingForDisplayDTO>> GetBorrowingsAsync()
        {
            var borrowings = await _unitOfWork.BorrowingRepository.GetQueryableBorrowingsAsync();

            var borrowingsDTO = await borrowings
                .Where(b => b.IsCanceled == false)
                .ProjectTo<BorrowingForDisplayDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return borrowingsDTO;
        }

    }
}
