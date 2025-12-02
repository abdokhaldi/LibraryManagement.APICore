using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    
    public class SmallPersonRepository : ISmallPersonRepository
    {
        private readonly LibraryDbContext _context;
        public SmallPersonRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable<SmallPersonEntity>> GetQueryableAllSmallPeopleAsync()
        {
            var smallPeopleList = _context.SmallPeople.AsQueryable();
            return Task.FromResult(smallPeopleList);
        }
    }  
}
