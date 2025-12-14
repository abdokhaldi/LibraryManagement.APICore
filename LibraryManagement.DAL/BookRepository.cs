using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace LibraryManagement.DAL
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
       public  BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public  Task<IQueryable<Book>> GetQueryableBooksAsync()
        {
            var listBooks = _context.Books.Include(c => c.Category)
                                           .AsNoTracking();
                return Task.FromResult(listBooks);
        }


        public async Task<Book?> GetBookForUpdateAsync(int bookID)
        {
            
                var book = await _context.Books
                    .Include(c=>c.Category)
                    .Where(b => b.BookID == bookID)
                    .FirstOrDefaultAsync();
                return book;
            }

        public async Task<Book?> GetBookForReadOnlyAsync(int bookID)
        {

            var book = await _context.Books.AsNoTracking()
                .Include(c => c.Category)
                .Where(b => b.BookID == bookID)
                .FirstOrDefaultAsync();
            return book;
        }

        public Task AddNewBookAsync(Book bookEntity)
        {
            _context.Books.Add(bookEntity);
            return Task.CompletedTask;
        }
           

      
           
        


        public async Task<int?> GetBookQuantityAsync(int bookID)
        {
            
                var quantity = await _context.Books.AsNoTracking()
                               .Where(b=>b.BookID == bookID)
                               .Select(b=>(int?)b.Quantity)
                              .FirstOrDefaultAsync();
                return quantity;
            }

       public async Task<bool> IsTitleExistsAsync(string title)
        {
            return await _context.Books.AsNoTracking()
                .AnyAsync(b=>b.Title.ToLower() == title.ToLower());
        }
        public async Task<bool> IsTitleExistsAsync(int id,string title)
        {
            return await _context.Books.AsNoTracking()
                .AnyAsync(b =>b.BookID == id && b.Title.ToLower() == title.ToLower());
        }
    }
}
