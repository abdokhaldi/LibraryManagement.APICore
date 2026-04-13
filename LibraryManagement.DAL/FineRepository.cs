using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Base;
using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
namespace LibraryManagement.DAL
{
    public class FineRepository : IFineRepository
    {
        private LibraryDbContext _context ;
        public FineRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Task<List<Fine>> GetFinesAsync()
        {
            var fines =  _context.Fines.ToListAsync();
            return fines;
        }

        public Task AddNewFineAsync(Fine fineEntity)
        {
            _context.Fines.Add(fineEntity);
            return Task.CompletedTask;
        }  

        public async Task<Fine?> GetFineByIdAsync(int id )
        {
            var fine = await _context.Fines.FindAsync(id);

            return fine;
        }

        public async Task<PagedList<Fine>> GetFinesAsync(FineParameters parameters)
        {
            var query = _context.Fines.AsNoTracking()
                .Include(f => f.Member)
                .ThenInclude(m => m.Person).AsQueryable();

            if (parameters.MemberID.HasValue)
            {
                query = query.Where(f => f.MemberID == parameters.MemberID.Value);
            }

            if (parameters.BorrowingID.HasValue)
            {
                query = query.Where(f => f.BorrowingID == parameters.BorrowingID.Value);
            }

            if (!string.IsNullOrEmpty(parameters.Status)) 
            {
                query = query.Where(f => f.Status == parameters.Status);
            }

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim().ToLower();
                query = query.Where(
                    f => f.Member.Person.FirstName.Trim().Contains(searchTerm)
                || f.Member.Person.LastName.Trim().Contains(searchTerm)
                || f.Status.Trim().Contains(searchTerm));
                    
            }

             query.ApplySort(parameters.OrderBy);

            var pagedList = await query.ToPagedListAsync(parameters.PageNumber, parameters.PageSize);
            return pagedList;
        }
    }
}
