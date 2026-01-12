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

       public async Task<List<Category>> GetCategoriesAsync()
        {
            
                var categories = await _context.Categories
                                .AsNoTracking()
                                .ToListAsync();

                return categories;
            
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
        public Task AddNewCategoryAsync(Category category) {
            _context.Categories.Add(category);
            return Task.CompletedTask;
        }
        public Task DeleteCategoryAsync(Category category) {
            _context.Categories.Remove(category);
            return Task.CompletedTask;
        }
        public async Task<bool> IsCategoryExistsAsync(string categoryName)
        {
            return await _context.Categories.AnyAsync(c =>
                           c.CategoryName.ToLower() == categoryName.ToLower());
        }
    }
}
