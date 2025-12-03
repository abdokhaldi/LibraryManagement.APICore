using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
      public  UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
        
    }
}
