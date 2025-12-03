using LibraryManagement.DAL.Context;
using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LibraryManagement.DAL.Interfaces;


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
                                             .Include(u=>u.Person)
                                             .Include(u => u.Role)
                                             .FirstOrDefaultAsync(u =>u.Username==username);
                return user;
         }
           

        public  Task<IQueryable<User>> GetQueryableUsersAsync()
        {
           
                var query = _context.Users
                                 .Include(u => u.Person)
                                 .Include(u => u.Role).AsNoTracking();
                                 
                return Task.FromResult(query);
            }
           

        public async Task<bool>  IsUsernameExistsAsync(string username)
        {
            
                var exists = await _context.Users.Where(u => u.Username == username).AsNoTracking()
                                           .Select(u => u.Username)
                                           .FirstOrDefaultAsync();
                return (exists != null);
            }
            

        public Task AddNewUser(User userEntity)
        {
               _context.Users.Add(userEntity);
                return Task.CompletedTask;
        }
            

        public Task UpdateUserAsync(User userEntity)
        {
            
                _context.Users.Update(userEntity);
               return Task.CompletedTask;
        }
           
    }
}
