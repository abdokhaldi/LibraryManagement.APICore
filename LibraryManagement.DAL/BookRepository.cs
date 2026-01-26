using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Shared.Parameters;
using System.Data;



namespace LibraryManagement.DAL
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
       public  BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public  IQueryable<Book> GetBookQuery(BookParameters parameters)
        {
            var query = _context.Books
                .Include(c => c.Category)
                .AsNoTracking() ;

            if (parameters.CategoryID.HasValue && parameters.CategoryID != 0)
            {
               query = query.Where(b => b.CategoryID == parameters.CategoryID);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                string searchTerm = parameters.SearchTerm.Trim().ToLower();
               query = query.Where(b => b.Title.ToLower().Contains(searchTerm) 
                || b.Author.ToLower().Contains(searchTerm));
            }

            return query;
        }


        public async Task<Book?> GetBookForUpdateAsync(int bookID)
        {
            
                var book = await _context.Books
                    .Include(c=>c.Category)
                    .Where(b => b.BookID == bookID && b.IsActive == true)
                    .FirstOrDefaultAsync();
                return book;
            }

        public async Task<Book?> GetBookForReadOnlyAsync(int bookID)
        {

            var book = await _context.Books.AsNoTracking()
                .Include(c => c.Category)
                .Where(b => b.BookID == bookID && b.IsActive==true)
                .FirstOrDefaultAsync();
            return book;
        }

        public Task AddNewBookAsync(Book bookEntity)
        {
            
            _context.Books.Add(bookEntity);
            return Task.CompletedTask;
        }
           

      
        public async Task<int> GetBookQuantityAsync(int bookID)
        {
            
                var quantity = await _context.Books.AsNoTracking()
                               .Where(b=>b.BookID == bookID)
                               .Select(b=>b.Quantity )
                              .FirstOrDefaultAsync();
                return quantity;
            }

       public async Task<bool> IsTitleExistsAsync(string title)
        {
            return await _context.Books
                .AnyAsync(b=>b.Title.ToLower() == title.ToLower());
        }
        public async Task<bool> IsTitleExistsAsync(int id,string title)
        {
            return await _context.Books
                .AnyAsync(b =>b.BookID == id && b.Title.ToLower() == title.ToLower());
        }
    }
}
