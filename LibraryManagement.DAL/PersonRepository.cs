using System;
using LibraryManagement.DAL.Entities;
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
    public class PersonRepository
    {
        private readonly LibraryDbContext _context; 
        public PersonRepository(LibraryDbContext context)
        {
            _context = context;
        }


         public async Task<List<Person>> GetAllPeopleAsync()
          {
            try
            {
                var peopleList = await _context.People.ToListAsync();
                return peopleList;
            }
            catch (DbException ex)
            {
                throw new Exception("Database error occurred while retrieving all people.", ex);
            }
         }
        public async Task<Person?> FindPersonByIDAsync(int personID)
        {

            try
            {
                var person = await _context.People.FindAsync(personID);
                return person;
            }
            catch (DbException ex)
            {
                throw new Exception("Database error occurred while retrieving person.", ex);
            }
            catch (Exception ex) 
            {

                throw;
                
            }
           
        }
        public async Task<int> AddNewPersonAsync(Person personEntity)
        {
            try
            {
                _context.People.Add(personEntity);
                await _context.SaveChangesAsync();
                return personEntity.PersonID;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public async Task<int> UpdatePersonAsync(Person personEntity)
        {
           
            try
            {
                _context.People.Update(personEntity);
               int rowsAffected =  await _context.SaveChangesAsync();
                return rowsAffected;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            
        }
        
        public async Task<int> SetPersonStatusAsync(int personID,bool isActive)
        {
            try
            {
                var personToChangeStatus = await _context.People.FirstOrDefaultAsync(p => p.PersonID == personID);
                if (personToChangeStatus == null)
                {
                    return 0;
                }
                if(personToChangeStatus.IsActive == isActive)
                {
                    return 1;
                }

                personToChangeStatus.IsActive = isActive;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
        }

        
    }
}
