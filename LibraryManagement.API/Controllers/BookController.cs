using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.BookDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LibraryManagement.API.Common;
using LibraryManagement.Shared.Parameters;
using System.Text.Json;
using LibraryManagement.Shared.HEADER_KEYS;
using Microsoft.AspNetCore.Hosting;

namespace LibraryManagement.API.Controllers
{
   // [Authorize(Roles = "Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _env;
        public BookController(IBookService bookService, IWebHostEnvironment env)
        {
            _bookService = bookService;
            _env = env;
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateBook([FromForm] BookForCreationDTO bookDTO)
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

        public async Task<IActionResult> UpdateBook(int id,[FromForm] BookForUpdateDTO bookDTO)
        {
           
            var result = await _bookService.UpdateBookAsync(id, bookDTO, _env.WebRootPath);
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
        public async Task<IActionResult> GetActiveBooks([FromQuery] BookParameters parameters)
        {
            var pagedBooks = await _bookService.GetActiveBooksAsync(parameters);

            Response.Headers.Append(HeaderKeys.Pagination , JsonSerializer.Serialize(pagedBooks.Metadata));
                
                return Ok(pagedBooks.Items);
        }




    }
}

