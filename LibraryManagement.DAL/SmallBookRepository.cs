using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;


namespace LibraryManagement.DAL
{
    public class SmallBookRepository : ISmallBookRepository
    {
        private readonly LibraryDbContext _context;
        public SmallBookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public  Task<IQueryable<SmallBookEntity>> GetQueryableSmallBooksAsync()
        {
            var smallBooksList =  _context.SmallBooks.AsQueryable();
            return Task.FromResult(smallBooksList);
        }
        
    }
}
