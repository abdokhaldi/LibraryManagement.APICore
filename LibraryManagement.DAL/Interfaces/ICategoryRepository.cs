using LibraryManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();  
        Task<Category?> GetCategoryForReadOnlyAsync(int categoryID);
        Task<Category?> GetCategoryForUpdateAsync(int categoryID);
        Task AddNewCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<bool> IsCategoryExistsAsync(string categoryName);
    }
}
