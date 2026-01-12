using LibraryManagement.DTO.CategoryDTOs;
using LibraryManagement.DTO.OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<OperationResult<CategoryForDisplayDTO>> GetCategoryDetailsAsync(int id);
        Task<List<CategoryForDisplayDTO>> GetAllCategoriesAsync();
        Task<OperationResult> UpdateCategoryAsync(int id, CategoryForUpdateDTO category);
        Task<OperationResult<int>> CreateCategoryAsync(CategoryForCreationDTO category);
        Task<OperationResult> DeleteCategoryAsync(int id);
    }
}
