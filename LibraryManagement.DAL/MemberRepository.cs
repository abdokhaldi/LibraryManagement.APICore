
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using System.Data;

using LibraryManagement.DAL.Context;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.DAL.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace LibraryManagement.DAL
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        // this is a generic method to get member by SettingsID and NationalNumber
        public async Task<Member?> GetMemberAsync(Expression<Func<Member,bool>> predicate, bool isTracked=false)
        {
            IQueryable<Member> query = _context.Members;
            if (!isTracked)
            {
                query = query.AsNoTracking();
            }
            var member = await query.Where(predicate)
                    .FirstOrDefaultAsync();
            return member;    
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
       

    
        public async Task<Member?> GetMemberByPersonIDAsync(Guid personID)
        {
            
              var member = await _context.Members.AsTracking()
                .Include(m => m.Person)
                 .Include(m=> m.Fines)
                .Where(m => m.PersonID == personID)
                .FirstOrDefaultAsync();

                return member;
            }
        public async Task<bool> IsMemberExists(Guid personID)
        {
            return await _context.Members
                .AsNoTracking()
                .AnyAsync(m => m.PersonID == personID && m.IsActive==true);
            
        }


    }
}
