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
           
        }
    }

