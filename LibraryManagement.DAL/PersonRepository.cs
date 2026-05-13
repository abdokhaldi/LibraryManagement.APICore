using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using System.Data;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.DAL.Base;
using LibraryManagement.Shared.Helpers;
using System.Linq.Expressions;

namespace LibraryManagement.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LibraryDbContext _context; 
        public PersonRepository(LibraryDbContext context)
        {
            _context = context;
        }
        
        public async Task<Person?> GetPersonAsync(Expression<Func<Person, bool>> predicate, bool trackChanges = false)
        {
            IQueryable<Person> query = _context.People;
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            var person = await query.Where(predicate)
                         .FirstOrDefaultAsync();
            return person;
        }

        public async Task<bool> CheckPersonExistenceAsync(Expression<Func<Person, bool>> predicate)
        {
            bool isFound = await _context.People.AnyAsync(predicate);

            return isFound; 
        }
        

        public async Task<PagedList<Person>> GetActivePeopleAsync(PersonParameters parameters)
          {
            
                var query = _context.People.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim();

                query = query.Where( p => 
                    p.FirstName.Contains(parameters.SearchTerm)
                    || p.LastName.Contains(parameters.SearchTerm)
                    || p.Phone.Contains(parameters.SearchTerm)
                    || p.Email.Contains(parameters.SearchTerm)
                    || p.Address.Contains(parameters.SearchTerm)
                    || p.City.Contains(parameters.SearchTerm)
                    );
            }

            query = query.ApplySort(parameters.OrderBy);

           
            return await query.ToPagedListAsync(parameters.PageNumber, parameters.PageSize);
         }
            
        public async Task<Person?> GetPersonForReadOnlyAsync(Guid personID)
        {
               var person = await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(
                   p => p.PersonID == personID && p.IsActive==true );
                return person;
            }

        public async Task<Person?> GetPersonForUpdateAsync(Guid personID)
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



        public async Task<(bool isNotFound,bool isNotActive)> CheckPersonStatus(Guid id)
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
