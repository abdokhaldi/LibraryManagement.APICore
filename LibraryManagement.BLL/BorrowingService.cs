using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.MemberDTOs;
namespace LibraryManagement.BLL
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        public BorrowingService(IUnitOfWork unitOfWork,IMemberService memberService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memberService = memberService;
        }

      public async Task<int> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO)
        {
            var bookEntity = await _unitOfWork.BookRepository.GetBookForUpdateAsync(borrowingDTO.BookID);

            if (bookEntity ==null || bookEntity.Quantity<= 0)
            {
                return 0;
            }
            var memberToCreate = new MemberForCreationDTO { PersonID = borrowingDTO.PersonID, JoinDate = DateTime.Now, IsActive = true };
                
            var memberEntity = await _memberService.CreateMemberAsync(memberToCreate);

            bookEntity!.Quantity --;

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

    }
}
