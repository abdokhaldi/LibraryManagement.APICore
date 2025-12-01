using LibraryManagement.DAL.Context;
using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace LibraryManagement.DAL
{
    public class UserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIDAsync(int userID)
        {
            try
            {
            var user = await _context.Users
                                         .Include(u => u.Person)
                                         .Include(u => u.Role)
                                         .FirstOrDefaultAsync(u=>u.UserID==userID);
                return user;
            }
            catch (DbException ex)
            {
                throw new Exception("Database Error occurred while retrieving user ." + ex);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _context.Users 
                                             .Include(u=>u.Person)
                                             .Include(u => u.Role)
                                             .FirstOrDefaultAsync(u =>u.Username==username);
                return user;
            }
            catch (DbException ex)
            {
                throw new Exception("Database Error occurred while retrieving user ." + ex);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var usersList = await _context.Users
                                 .Include(u=>u.Person)
                                 .Include(u=>u.Role)
                                 .ToListAsync();
                return usersList;
            }
            catch (DbException ex)
            {
                throw ;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool>  IsUsernameExistsAsync(string username)
        {
            try
            {
                var exists = await _context.Users.Where(u => u.Username == username)
                                           .Select(u => u.Username)
                                           .FirstOrDefaultAsync();
                return (exists != null);
            }
            catch (DbException ex)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> AddNewUser(User userEntity)
        {
            try {
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();
                return userEntity.UserID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateUserAsync(User userEntity)
        {
            try
            {
                _context.Users.Update(userEntity);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SetUserActiveStatusAsync(int userID,bool isActive)
        {
            try
            {
                var userToChangeStatus = await _context.Users.FindAsync(userID);
                if (userToChangeStatus == null)
                {
                    return 0;
                }
                if (userToChangeStatus.IsActive == isActive)
                {
                    return 1;
                }
                userToChangeStatus.IsActive = isActive;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SetUserBlockStatusAsync(int userID,bool isBlocked)
        {
            try
            {
                var userToChangeStatus = await _context.Users.FindAsync(userID);
                if (userToChangeStatus == null)
                {
                    return 0;
                }
                if (userToChangeStatus.IsBlocked == isBlocked)
                {
                    return 1;
                }
                userToChangeStatus.IsBlocked = isBlocked;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
