using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Interfaces
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
