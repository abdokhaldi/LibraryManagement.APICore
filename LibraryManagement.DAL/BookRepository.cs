using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace LibraryManagement.DAL
{
    public class BookRepository
    {
        private readonly LibraryDbContext _context;
       public  BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                var listBooks = await _context.Books
                                                   .Include(c => c.Category)
                                                   .ToListAsync();
                return listBooks;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while retrieving data .{ex}");
            }
        }


        public async Task<Book?> FindBookByIDAsync(int bookID)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookID);
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error occurred while retrieving data .{ex}");
            }
        }
           
        


        public async Task<int> AddNewBookAsync(Book bookEntity)
        {
            try
            {
             _context.Books.Add(bookEntity);
             await _context.SaveChangesAsync();
             return bookEntity.BookID;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
          }

        public async Task<int> UpdateBookAsync(Book bookEntity)
        {
            try
            {
                _context.Books.Update(bookEntity);
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


        public async Task<int?> GetBookQuantityAsync(int bookID)
        {
            try
            {
                var quantity = await _context.Books
                               .Where(b=>b.BookID == bookID)
                               .Select(b=>(int?)b.Quantity)
                              .FirstOrDefaultAsync();
                return quantity;
            }
            catch (DbException ex)
            {
                throw new Exception ($"An error occurred while retrieving book quantity .{ex}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static int SetBookStatusAsync(int bookID, bool isActive)
        {
            string query = @"UPDATE Books 
                             SET IsActive = @isActive WHERE BookID=@bookID;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@bookID"] = (SqlDbType.Int, bookID, null),
                ["@isActive"] = (SqlDbType.Bit, isActive, null),
            };

            int rowsAffected = (int)SqlHelper.ExecuteCommand(query, CommandType.Text, SqlHelper.ExecuteType.ExecuteNonQuery, parameters);
            return rowsAffected;
        }


    }
}
