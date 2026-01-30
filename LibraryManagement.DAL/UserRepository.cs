using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.DAL.Base;

namespace LibraryManagement.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserForUpdateAsync(int userID)
        {
            
            var user = await _context.Users
                                         .Include(u => u.Person)
                                         .Include(u => u.Role)
                                         .FirstOrDefaultAsync(u=>u.UserID==userID);
                return user;
            }

        public async Task<User?> GetUserForReadOnlyAsync(int userID)
        {

            var user = await _context.Users.AsNoTracking()
                                         .Include(u => u.Person)
                                         .Include(u => u.Role)
                                         .FirstOrDefaultAsync(u => u.UserID == userID);
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.AsNoTracking()
                               .Include(p=>p.Person)
                               .Include(r=>r.Role)
                               .FirstOrDefaultAsync(u =>u.Username==username);
                return user;
         }

      public async Task<User?> GetUserForLoginAsync(string identifier)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .Include(u=>u.Role)
                .Where(u => u.Username == identifier
                || u.Person.Email == identifier)
                .FirstOrDefaultAsync();

            return user;
        }

        public IQueryable<User> GetQueryableUsers(UserParameters parameters)
        {
           
                var query = _context.Users
                                 .AsNoTracking()
                                 .Include(u => u.Person)
                                 .Include(u => u.Role)
                                 .AsQueryable();

            if (parameters.PersonID.HasValue && parameters.PersonID != 0)
            {
                query = query.Where(u => u.PersonID == parameters.PersonID);               
            }

            if (parameters.IsBlocked)
            {
                query = query.Where(u => u.IsBlocked == parameters.IsBlocked);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim().ToLower();
                query = query.Where(u =>

                   u.Username.ToLower().Contains(searchTerm)
                || u.Person.FirstName.ToLower().Contains(searchTerm)
                || u.Person.LastName.ToLower().Contains(searchTerm)
                || u.Person.Phone.ToLower().Contains(searchTerm)
                || u.Person.Email.ToLower().Contains(searchTerm)
                || u.Person.Address.ToLower().Contains(searchTerm)
                || u.Person.City.ToLower().Contains(searchTerm)
                );
            }

            query = query.ApplySort(parameters.OrderBy);

            return query;
        }
           

        public async Task<bool>  IsUsernameExistsAsync(string username)
        {

           return await _context.Users.AnyAsync(u=>u.Username == username);
           
        }

        public async Task<bool> IsUsernameExistsForUpdateAsync(int id, string username)

        {

            var exists = await _context.Users.AnyAsync(u =>
            u.Username == username 
            && u.UserID != id
            );
            return exists;
        }
        public Task AddNewUserAsync(User userEntity)
        {
               _context.Users.Add(userEntity);
                return Task.CompletedTask;
        }
            

        public Task UpdateUserAsync(User userEntity)
        {
            
                _context.Users.Update(userEntity);
               return Task.CompletedTask;
        }

        public async Task<User?> GetUserAsPersonAsync(int personID)
        {
            return await _context.Users.AsNoTracking()
                                           .Where(u => u.PersonID == personID)
                                           .FirstOrDefaultAsync();
           
        }
    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
           return await _context.Users
                                .Include(u=>u.Person)
                                .Include(u => u.Role)
                                .Include(u => u.RefreshTokens)
                                .SingleOrDefaultAsync( u => u.RefreshTokens 
                                .Any(t => t.Token == refreshToken));   
        }

    }
}
