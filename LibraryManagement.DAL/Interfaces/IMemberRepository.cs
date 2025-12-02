using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IMemberRepository
    {
        Task<IQueryable<Member>> GetQueryableMembersAsync();

        Task<Member?> GetMemberByIDAsync(int memberID);

        Task<Member?> FindMemberByPersonIDAsync(int personID);

        Task<int> AddNewMemberAsync(Member memberEntity);

        Task<int> UpdateMemberAsync(Member memberEntity);

        Task<int> SetMemberStatusAsync(int memberID, bool isActive);
    }
}