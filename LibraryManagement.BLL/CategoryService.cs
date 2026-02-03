using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.CategoryDTOs;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.OperationResults;

namespace LibraryManagement.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      public async Task<OperationResult<CategoryForDisplayDTO>> GetCategoryDetailsAsync(int id){
            var category = await _unitOfWork.CategoryRepository.GetCategoryForReadOnlyAsync(id);
            if (category == null)
            {
                return OperationResult<CategoryForDisplayDTO>.Failure(OperationStatus.NotFound, $"The category with id:{id} was not found .");
            }
            var categoryDTO = _mapper.Map<CategoryForDisplayDTO>(category);
            return OperationResult<CategoryForDisplayDTO>.Success(categoryDTO);
        }
      public async Task<List<CategoryForDisplayDTO>> GetAllCategoriesAsync(){ 

            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            var categoriesDTO = _mapper.Map<List<CategoryForDisplayDTO>>(categories);
               
                
            return categoriesDTO;
        }
      public async Task<OperationResult> UpdateCategoryAsync(int id, CategoryForUpdateDTO categoryDTO)
      {
            var categoryForUpdate = await _unitOfWork.CategoryRepository.GetCategoryForUpdateAsync(id);
            if (categoryForUpdate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The categoryDTO with id:{id} is not found for update");
            }
            if (!string.IsNullOrEmpty(categoryDTO.CategoryName))
            {
                if (!categoryDTO.CategoryName.Equals(categoryForUpdate.CategoryName))
                {
                    bool isExists = await _unitOfWork.CategoryRepository.IsCategoryExistsAsync(categoryDTO.CategoryName);
                    if (isExists)
                    {
                        return OperationResult.Failure(OperationStatus.Conflict, $"cannot update category to the same category that already existing in the system .");
                    }
                }
            }
            _mapper.Map(categoryDTO, categoryForUpdate);
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
      public async Task<OperationResult<int>> CreateCategoryAsync(CategoryForCreationDTO categoryDTO){
            bool isExists = await _unitOfWork.CategoryRepository.IsCategoryExistsAsync(categoryDTO.CategoryName);
            if (isExists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, $"The categoryDTO with name : {categoryDTO.CategoryName} is already exists .");
            }
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _unitOfWork.CategoryRepository.AddNewCategoryAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();
            return OperationResult<int>.Success(categoryEntity.CategoryID);
        }
      public async Task<OperationResult> DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _unitOfWork.CategoryRepository.GetCategoryForReadOnlyAsync(id);
            if (categoryToDelete == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The category with id: {id} is not found to delete");
            }

            await _unitOfWork.CategoryRepository.DeleteCategoryAsync(categoryToDelete);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
        }
    }
}
