using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.BLL
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
      public async Task<Member> CreateMemberAsync(MemberForCreationDTO memberDTO)
        {
            var existingMember = await _unitOfWork.MemberRepository.GetMemberByPersonIDAsync(memberDTO.PersonID);
              
            if (existingMember != null)
            {
                return existingMember;
            }
            var person = await _unitOfWork.PersonRepository.GetPersonForReadOnlyAsync(memberDTO.PersonID);
            if (person == null)
            {
                return null!;
            }
            var memberEntity = _mapper.Map<Member>(memberDTO);
            await _unitOfWork.MemberRepository.AddNewMemberAsync(memberEntity);
             
            return memberEntity;
        }

      public async Task<PagedList<MemberForDisplayDTO>> GetActiveMembersAsync(MemberParameters parameters)
        {
            var pagedMembers = await _unitOfWork.MemberRepository.GetActiveMembersAsync( parameters);

            var membersDTO = _mapper.Map<List<MemberForDisplayDTO>>(pagedMembers.Items);
           
            return pagedMembers.MapTo(membersDTO);

           }

      public async Task<MemberForDisplayDTO?> GetMemberDetails(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberForReadOnlyAsync(id);
            if (member == null)
            {
                return null;
            }
            var MemberDTO = _mapper.Map<MemberForDisplayDTO>(member);
            return MemberDTO;
        }
      public async Task<bool> DeactivateMember(int id)
        {
            var memberForDeactivate = await _unitOfWork.MemberRepository.GetMemberForUpdateAsync(id);
            if (memberForDeactivate == null)
            {
                return false;
            }
            if (!memberForDeactivate.IsActive)
            {
                return true;
            }
            memberForDeactivate.IsActive = false;
            
            return true;
        }

      public async Task<bool> ActivateMember(int id) 
        {
            var memberForActivate = await _unitOfWork.MemberRepository.GetMemberForUpdateAsync(id);
            if (memberForActivate == null)
            {
                return false;
            }
            if (memberForActivate.IsActive)
            {
                return true;
            }
            memberForActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

  ///    public async  Task<OperationResult<int>> GetMembersCountAsync()
  ///      {
  ///          int count = await _unitOfWork.MemberRepository.GetMembersCountAsync();
  ///          return OperationResult<int>.Success(count);
  ///      }

    }
}
