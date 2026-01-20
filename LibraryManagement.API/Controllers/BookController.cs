using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.BookDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LibraryManagement.API.Common;

namespace LibraryManagement.API.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreationDTO bookDTO)
        {
             var result = await _bookService.CreateNewBookAsync(bookDTO);
              
            if(result.IsSuccess)
               return CreatedAtAction(nameof(GetBookDetails), new { id = result.Data }, result.Data);
            return HandleErrorResult(result);
        }

        [AllowAnonymous]       
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookDetails(int id)
        {
           
            var result = await _bookService.GetBookDetailsAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);
           
                return HandleErrorResult(result);
            }

        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int) StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateBook(int id,[FromBody] BookForUpdateDTO bookDTO)
        {
            
            var result = await _bookService.UpdateBookAsync(id, bookDTO);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/DeactivateBook")]
        [ProducesResponseType((int) StatusCodes.Status204NoContent)]
        [ProducesResponseType((int) StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateBook(int id)
        {
            
            var result = await _bookService.DeactivateBookAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/ActivateBook")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActivateBook(int id)
        {
            
            var result = await _bookService.ActivateBookAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllActiveBooksAsync();
           
                return Ok(books);
        }




    }
}

