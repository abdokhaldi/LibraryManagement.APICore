
using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Context;

namespace LibraryManagement.DAL
{
    public class MemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            try{
                var membersList = await _context.Members.Include(m => m.Person)
                                                        .ToListAsync();
                return membersList;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
        }
       
       
        public async Task<Member?> GetMemberByIDAsync(int memberID)
        {
            try
            {
                var member = await _context.Members
                    .Include(m => m.Person)
                    .Where(m => m.MemberID == memberID)
                    .FirstOrDefaultAsync();
                return member;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> AddNewMemberAsync(Member memberEntity)
        {
            try {
                _context.Members.Add(memberEntity);
                await _context.SaveChangesAsync();
                return memberEntity.MemberID;

        }catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> UpdateMemberAsync(Member memberEntity)
        {
            try
            {
                _context.Members.Update(memberEntity);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    
        public async Task<Member?> FindMemberByPersonIDAsync(int personID)
        {
            try
            {
              var member = await _context.Members
                .Include(m => m.Person)
                .Where(m => m.PersonID == personID)
                .FirstOrDefaultAsync();

                return member;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task<int> SetMemberStatusAsync(int memberID,bool isActive)
        {
            try
            {
                var memberToChangeStatus = await _context.Members
                                  .FindAsync(memberID);
                if (memberToChangeStatus == null)
                {
                    return 0;
                }
                if (memberToChangeStatus.IsActive == isActive)
                {
                    return 1;
                }
                memberToChangeStatus.IsActive = isActive;
                _context.Members.Update(memberToChangeStatus);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
