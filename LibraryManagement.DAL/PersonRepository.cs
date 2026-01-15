using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using System.Data;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

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
               var person = await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(
                   p => p.PersonID == personID && p.IsActive==true );
                return person;
            }

        public async Task<Person?> GetPersonForUpdateAsync(int personID)
        {
            var person = await _context.People
                    .FindAsync(personID);
            return person;
        }
        public Task AddNewPersonAsync(Person personEntity)
        {
            _context.People.Add(personEntity);
            return Task.CompletedTask;
        }

        public async Task<(bool isNotFound,bool isNotActive)> CheckPersonStatus(int id)
        {
            var personStatus = await _context.People
                    .AsNoTracking()
                    .Where(p => p.PersonID == id)
                    .Select(p => new
                    {
                        IsActive =  p.IsActive
                    }
            ).FirstOrDefaultAsync();

            if (personStatus == null)
                return (isNotFound: true, isNotActive: false);

            return (isNotFound: false, isNotActive:!personStatus.IsActive);
        }
        
        
        public async Task<(bool EmailExists, bool PhoneExists)> IsEmailOrPhoneExistsAsync(string email,string phone)
        {
            var result = await _context.People
                .Where(p => p.Email == email || p.Phone == phone)
                .Select(p => new
                {
                    EmailMatch = p.Email == email,
                    PhoneMatch = p.Phone == phone
                })
                .FirstOrDefaultAsync();
            return (result?.EmailMatch?? false, result?.PhoneMatch ?? false);   
        }

        public async Task<bool> IsEmailExists(string email)
            => await _context.People.AnyAsync(p=>p.Email==email);

        public async Task<bool> IsPhoneExists(string phone)
            => await _context.People.AnyAsync(p => p.Phone == phone);
    }
}
