using System;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Context;
using LibraryManagement.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data.Common;
namespace LibraryManagement.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LibraryDbContext _context; 
        public PersonRepository(LibraryDbContext context)
        {
            _context = context;
        }


         public Task<IQueryable<Person>> GetQueryablePeopleAsync()
          {
            
                var query = _context.People.AsNoTracking();
                return Task.FromResult(query);
            }
            
        public async Task<Person?> GetPersonForReadOnlyAsync(int personID)
        {
               var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(p=>p.PersonID == personID);
                return person;
            }
        public async Task<Person?> GetPersonForUpdateAsync(int personID)
        {
            var person = await _context.People.FindAsync(personID);
            return person;
        }
        public Task AddNewPersonAsync(Person personEntity)
        {
           
            _context.People.Add(personEntity);
            return Task.CompletedTask;
        }
            

        public Task UpdatePersonAsync(Person personEntity)
        {
            _context.People.Update(personEntity);
            return Task.CompletedTask;
        }
         
        
        
    }
}
