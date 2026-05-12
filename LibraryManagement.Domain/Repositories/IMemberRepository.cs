using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<PagedList<Member>> GetActiveMembersAsync(MemberParameters parameters);

        Task<Member?> GetMemberForUpdateAsync(int memberID);
        Task<Member?> GetMemberForReadOnlyAsync(int memberID);

        Task<Member?> GetMemberByPersonIDAsync(Guid personID);

        Task AddNewMemberAsync(Member memberEntity);

        Task<bool> IsMemberExists(Guid personID);

       // Task<int> GetMembersCountAsync();
    }
}