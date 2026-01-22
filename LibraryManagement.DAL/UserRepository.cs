using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Interfaces;


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

        public Task<IQueryable<User>> GetQueryableUsersAsync()
        {
           
                var query = _context.Users
                                 .Include(u => u.Person)
                                 .Include(u => u.Role).AsNoTracking();
                                 
                return Task.FromResult(query);
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
