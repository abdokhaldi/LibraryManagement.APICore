using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.DAL.Base;
using LibraryManagement.Shared.Helpers;

namespace LibraryManagement.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserForUpdateAsync(Guid userID)
        {
            
            var user = await _context.Users
                                         .Include(u => u.Person)
                                         .Include(u => u.Role)
                                         .FirstOrDefaultAsync(u=>u.UserID==userID);
                return user;
            }
        public async Task<User?> GetUserForReadOnlyAsync(Guid userID)
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
                .IgnoreQueryFilters()
               .Include(u => u.Person)
               .Include(u => u.Role)
               .Include(u => u.RefreshTokens)
               .FirstOrDefaultAsync(u => u.Username == identifier
               || u.Person.Email == identifier);
             return user;
        }
        public async Task<PagedList<User>> GetActiveUsersAsync(UserParameters parameters)
        {
           
                var query = _context.Users
                                 .AsNoTracking()
                                 .Include(u => u.Person)
                                 .Include(u => u.Role)
                                 .AsQueryable();

            if (parameters.PersonID != null && parameters.PersonID != Guid.Empty)
            {
                query = query.Where(u => u.PersonID == parameters.PersonID);               
            }

            if (parameters.IsBlocked)
            {
                query = query.Where(u => u.IsBlocked == parameters.IsBlocked);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim();
                query = query.Where(u =>

                   u.Username.Contains(searchTerm)
                || u.Person.FirstName.Contains(searchTerm)
                || u.Person.LastName.Contains(searchTerm)
                || u.Person.Phone.Contains(searchTerm)
                || u.Person.Email.Contains(searchTerm)
                || u.Person.Address.Contains(searchTerm)
                || u.Person.City.Contains(searchTerm)
                );
            }

            query = query.ApplySort(parameters.OrderBy);
            
            return await query.ToPagedListAsync(parameters.PageNumber, parameters.PageSize);
        }
        public async Task<bool>  IsUsernameExistsAsync(string username)
        {

           return await _context.Users.AnyAsync(u=>u.Username == username);
           
        }
        public async Task<bool> IsUsernameExistsForUpdateAsync(Guid id, string username)

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
        public async Task<User?> GetUserAsPersonAsync(Guid personID)
        {
            return await _context.Users.AsNoTracking()
                                           .Where(u => u.PersonID == personID)
                                           .FirstOrDefaultAsync();
           
        }
        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
           return await _context.Users
                                .IgnoreQueryFilters()
                                .Include(u=>u.Person)
                                .Include(u => u.Role)
                                .Include(u => u.RefreshTokens)
                                .SingleOrDefaultAsync( u => u.RefreshTokens 
                                .Any(t => t.Token == refreshToken));   
        }

    }
}
