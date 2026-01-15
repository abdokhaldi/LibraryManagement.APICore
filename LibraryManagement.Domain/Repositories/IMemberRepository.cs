using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<IQueryable<Member>> GetQueryableMembersAsync();

        Task<Member?> GetMemberForUpdateAsync(int memberID);
        Task<Member?> GetMemberForReadOnlyAsync(int memberID);

        Task<Member?> GetMemberByPersonIDAsync(int personID);

        Task AddNewMemberAsync(Member memberEntity);

        Task<bool> IsMemberExists(int id);
    }
}