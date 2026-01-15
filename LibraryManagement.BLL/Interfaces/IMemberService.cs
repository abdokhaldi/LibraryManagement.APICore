using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.Domain.Entities;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IMemberService
    {
       internal Task<Member> CreateMemberAsync(MemberForCreationDTO memberDTO );
        Task<List<MemberForDisplayDTO>> GetAllMembersAsync();
        Task<MemberForDisplayDTO?> GetMemberDetails(int id);
        Task<bool> DeactivateMember(int id);
        Task<bool> ActivateMember(int id);

    }
}
