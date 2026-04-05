using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDTO categoryDTO)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDTO);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetCategory), new { id = result.Data }, result.Data);
            return HandleErrorResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDTO categoryDTO)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryDTO);

            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

        [AllowAnonymous] 
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }


        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetCategoryDetailsAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);
            return HandleErrorResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

    }
}


