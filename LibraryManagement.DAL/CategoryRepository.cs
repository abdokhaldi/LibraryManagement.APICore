using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using System.Data.Common;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _context;
        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public  Task<IQueryable<Category>> GetCategoriesAsync()
        {
            
                var categoriesList =  _context.Categories.AsNoTracking();
                return Task.FromResult(categoriesList);
            
        }

        public async Task<Category?> GetCategoryForReadOnlyAsync(int categoryID)
        {
            
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c=>c.CategoryID == categoryID);
                return category;
        }
        public async Task<Category?> GetCategoryForUpdateAsync(int categoryID)
        {

            var category = await _context.Categories.FindAsync(categoryID);
            return category;
        }

    }
}
