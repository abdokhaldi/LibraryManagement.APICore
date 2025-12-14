using LibraryManagement.BLL;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO;
using LibraryManagement.DTO.BookDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }



        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreationDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
              var newBookId = await _bookService.CreateNewBookAsync(bookDTO);
            if (newBookId == null)
            {
                return BadRequest("This title is already exists , books must have a unique title.");

            }
               return CreatedAtAction(nameof(GetBookDetails), new { bookID = newBookId.Value }, newBookId);
        }

        [HttpGet("{bookID}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBookDetails(int bookID)
        {
           
            var book = await _bookService.GetBookDetailsAsync(bookID);
            if (book == null)
            {
               
                return NotFound($"The book with ID:{bookID} is not found");
            }
                return Ok(book);
            }

        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int) StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateBook(int id,[FromBody] BookForUpdateDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int updateResult = await _bookService.UpdateBookAsync(id, bookDTO);
            if (updateResult == 0)
            {
                return NotFound($"The book with ID : {id} not found for update .");
            }
            if (updateResult == -1)
            {
               return BadRequest($"The book with title:{bookDTO.Title} is already exists.");
            }
            return NoContent();
        }

        [HttpPatch("{id}/DeactivateBook")]
        [ProducesResponseType((int) StatusCodes.Status204NoContent)]
        [ProducesResponseType((int) StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateBook(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool deactivationResult = await _bookService.DeactivateBookAsync(id);
            if (deactivationResult == false)
            {
                return NotFound($"The book with ID:{id} not found for deactivate");
            }
            return NoContent();
        }

        [HttpPatch("{id}/ActivateBook")]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActivateBook(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool deactivationResult = await _bookService.ActivateBookAsync(id);
            if (deactivationResult == false)
            {
                return NotFound($"The book with ID:{id} not found for Activate");
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllActiveBooksAsync();
           
                return Ok(books);
        }




    }
}

