using LibraryManagement.DTO;
using LibraryManagement.DAL.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using LibraryManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL
{
    public class CategoryRepository
    {
        private readonly LibraryDbContext _context;
        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categoriesList = await _context.Categories.ToListAsync();
                return categoriesList;
            }
            catch(DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Category?> GetCategoryByIDAsync(int categoryID)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryID);
                return category;
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

    }
}
