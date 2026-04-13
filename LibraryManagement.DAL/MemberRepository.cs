
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using System.Data;

using LibraryManagement.DAL.Context;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.DAL.Base;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagement.DAL
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<Member>> GetActiveMembersAsync(MemberParameters parameters)
        {

            var query = _context.Members
                         .Include(m => m.Person)
                         .Include(m => m.Fines )
                         .AsNoTracking()
                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
             string searchTerm = parameters.SearchTerm.Trim();

             query = query.Where(m =>

                m.Person.FirstName.Contains(searchTerm)
             || m.Person.LastName.Contains(searchTerm)
             || m.Person.Phone.Contains(searchTerm)
             || m.Person.Email.Contains(searchTerm)
             || m.Person.Address.Contains(searchTerm)
             || m.Person.City.Contains(searchTerm)
             );
            }

            query = query.ApplySort(parameters.OrderBy);

            return query.ToPagedListAsync(parameters.PageNumber,parameters.PageSize);
            }
            
       
        public async Task<Member?> GetMemberForUpdateAsync(int memberID)
        {
            
                var member = await _context.Members
                    .Include(m => m.Person)
                    .Where(m => m.MemberID == memberID)
                    .FirstOrDefaultAsync();
                return member;
            }
        public async Task<Member?> GetMemberForReadOnlyAsync(int memberID)
        {

            var member = await _context.Members
                .AsNoTracking()
                .Include(m => m.Person)
                .Include(m => m.Fines)
                .Where(m => m.MemberID == memberID && m.IsActive==true)
                .FirstOrDefaultAsync();
            return member;
        }

        public Task AddNewMemberAsync(Member memberEntity)
        {

            _context.Members.Add(memberEntity);
            return Task.CompletedTask;
        }
       

    
        public async Task<Member?> GetMemberByPersonIDAsync(int personID)
        {
            
              var member = await _context.Members
                .Include(m => m.Person)
                .Where(m => m.PersonID == personID)
                .FirstOrDefaultAsync();

                return member;
            }
        public async Task<bool> IsMemberExists(int id)
        {
            return await _context.Members
                .AsNoTracking()
                .AnyAsync(m => m.PersonID == id && m.IsActive==true);
            
        }


      //  public async Task<int> GetMembersCountAsync()
      //  {
      //      return await _context.Members.CountAsync();
      //   }

    }
}
