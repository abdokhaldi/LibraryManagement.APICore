using LibraryManagement.DTO.MemberDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Entities;
namespace LibraryManagement.BLL.Interfaces
{
    public interface IMemberService
    {
        Task<Member> CreateMemberAsync(MemberForCreationDTO memberDTO );
        Task<List<MemberForDisplayDTO>> GetAllMembersAsync();
        Task<MemberForDisplayDTO?> GetMemberDetails(int id);
        Task<bool> DeactivateMember(int id);
        Task<bool> ActivateMember(int id);

    }
}
