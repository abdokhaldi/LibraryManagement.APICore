using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
    }
}
