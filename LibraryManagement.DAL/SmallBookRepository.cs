using LibraryManagement.DAL.Context;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;


namespace LibraryManagement.DAL
{
    public class SmallBookRepository
    {
        private readonly LibraryDbContext _context;
        public SmallBookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        
    }
}
