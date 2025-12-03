using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IMemberRepository
    {
        Task<IQueryable<Member>> GetQueryableMembersAsync();

        Task<Member?> GetMemberForUpdateAsync(int memberID);
        Task<Member?> GetMemberForReadOnlyAsync(int memberID);

        Task<Member?> GetMemberByPersonIDForReadOnlyAsync(int personID);

        Task AddNewMemberAsync(Member memberEntity);

        Task UpdateMemberAsync(Member memberEntity);

    }
}