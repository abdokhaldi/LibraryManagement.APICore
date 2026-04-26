using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IMemberService
    {
       internal Task<Member> CreateMemberAsync(Person person, MemberForCreationDTO memberDTO );
        Task<PagedList<MemberForDisplayDTO>> GetActiveMembersAsync(MemberParameters parameters);

        Task<MemberForDisplayDTO?> GetMemberDetails(int id);
        Task<bool> DeactivateMember(int id);
        Task<bool> ActivateMember(int id);

       // Task<OperationResult<int>> GetMembersCountAsync();

    }
}
