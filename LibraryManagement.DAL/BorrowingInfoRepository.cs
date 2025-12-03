using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DAL.Entities;

namespace LibraryManagement.DAL
{
    public class BorrowingInfoRepository : IBorrowingInfoRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowingInfoRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public  Task<IQueryable<BorrowingInfoView>> GetQueryableFullBorrowingsInfoAsync()
        {
            var query = _context.FullBorrowingsInfo.AsNoTracking();

            return Task.FromResult(query); 
        }
    }
}