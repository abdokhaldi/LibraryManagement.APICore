
using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Context;

namespace LibraryManagement.DAL
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public  Task<IQueryable<Member>> GetQueryableMembersAsync()
        {

            var query = _context.Members.Include(m => m.Person).AsNoTracking();
                                                        
            return Task.FromResult(query);
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

            var member = await _context.Members.AsNoTracking()
                .Include(m => m.Person)
                .Where(m => m.MemberID == memberID)
                .FirstOrDefaultAsync();
            return member;
        }

        public Task AddNewMemberAsync(Member memberEntity)
        {

            _context.Members.Add(memberEntity);
            return Task.CompletedTask;
        }
       

        public Task UpdateMemberAsync(Member memberEntity)
        {
            
                _context.Members.Update(memberEntity);
            return Task.CompletedTask;
            }
            
            
    
        public async Task<Member?> GetMemberByPersonIDForReadOnlyAsync(int personID)
        {
            
              var member = await _context.Members.AsNoTracking()
                .Include(m => m.Person)
                .Where(m => m.PersonID == personID)
                .FirstOrDefaultAsync();

                return member;
            }
            

    }
}
