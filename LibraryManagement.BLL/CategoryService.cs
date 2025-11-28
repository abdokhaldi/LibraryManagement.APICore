using System;
using DAL_LibraryManagement;
using DTO_LibraryManagement;
using System.Collections.Generic;
using System.Linq;

namespace BLL_LibraryManagement
{
    public class CategoryService
    {
        public int CategoryID{get;set;}
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public CategoryService(CategoryDTO category)
        {
            CategoryID = category.CategoryID;
            CategoryName = category.CategoryName;
            Description = category.Description;
        }

        public static List<CategoryService> GetAllCategories()
        {
            List<CategoryDTO> categories = CategoryRepository.GetAllCategories();
           return categories.Select(c => new CategoryService(c)).ToList();
        }

        public static CategoryService GetCategoryByID(int categoryID)
        {
            CategoryDTO category = CategoryRepository.GetCategoryByID(categoryID);

            return new CategoryService(category);
        }
    }
}
