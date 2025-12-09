using System;
using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.BLL.Interfaces;

namespace LibraryManagement.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
