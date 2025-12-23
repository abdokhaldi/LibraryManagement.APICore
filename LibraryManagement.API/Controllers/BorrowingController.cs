using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.BorrowingDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> RecordNewBorrowing([FromBody] BorrowingForCreationDTO borrowingDTO)
        {
            int newBorrowingID = await _borrowingService.CreateBorrowingAsync(borrowingDTO);
            if (newBorrowingID == 0)
            {
                return BadRequest("No book to borrow");
            }
            return CreatedAtAction(nameof(GetBorrowingDetails), new { id = newBorrowingID }, newBorrowingID);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBorrowingDetails(int id)
        {
            var borrowing = await _borrowingService.GetBorrowingDetailsAsync(id);
            if (borrowing == null)
            {
                return NotFound($"The borrowing with ID:{id} not found .");
            }
            return Ok(borrowing);
        }

        [HttpPatch("{id}/ReturnBook")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var (success, error) = await _borrowingService.ReturnBookAsync(id);
            if (!success)
            {
                if (error.ToLower().Contains("not found"))
                {
                    return NotFound(error);
                }
                return BadRequest(error);
            }
            return NoContent();

        }

        [HttpPatch("{id}/ExtendDueDate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExtendDueDate(int id, [FromBody] BorrowingForExtendDTO borrowingDTO)
        {
            var (success, error) = await _borrowingService.ExtendDueDateAsync(id,borrowingDTO);
            if (!success)
            {
                if (error.ToLower().Contains("not found"))
                {
                    return NotFound(error);
                }
                return BadRequest(error);
            }
            return NoContent();
        
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBorrowings()
        {
            var borrowings = await _borrowingService.GetBorrowingsAsync();
            return Ok(borrowings);
         }
        
        [HttpGet("/GetOverdue")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOverdue()
        {
            var borrowings = await _borrowingService.GetOverdueAsync();
            return Ok(borrowings);
        }



    }
}
