using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.BorrowingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LibraryManagement.API.Common;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.HEADER_KEYS;
using System.Text.Json;

namespace LibraryManagement.API.Controllers
{
    // [Authorize(Roles ="Admin,Librarian")]
    [AllowAnonymous]
    [ApiController] 
    [Route("api/[controller]")]
    
    public class BorrowingController : BaseController
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
            var result = await _borrowingService.CreateBorrowingAsync(borrowingDTO);
            if (result.IsSuccess)
              return CreatedAtAction(nameof(GetBorrowingDetails), new { id = result.Data }, result.Data);

            return HandleErrorResult(result);
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBorrowingDetails(int id)
        {
            var result = await _borrowingService.GetBorrowingDetailsAsync(id);
            if (result.IsSuccess)
               return Ok(result.Data);
            return HandleErrorResult(result);
            
        }

        [HttpPatch("{id}/ReturnBook")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var result = await _borrowingService.ReturnBookAsync(id);
            if (result.IsSuccess)
             
            return NoContent();
            return HandleErrorResult(result);

        }

        [HttpPatch("{id}/ExtendDueDate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExtendDueDate(int id, [FromBody] BorrowingForExtendDTO borrowingDTO)
        {
            var result = await _borrowingService.ExtendDueDateAsync(id,borrowingDTO);
            if (result.IsSuccess)
            
               return NoContent();
            return HandleErrorResult(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBorrowings([FromQuery]BorrowingParameters parameters)
        {
            var pagedBorrowings = await _borrowingService.GetBorrowingsAsync(parameters);
            Response.Headers.Append(HeaderKeys.Pagination, JsonSerializer.Serialize(pagedBorrowings.Metadata));
            return Ok(pagedBorrowings.Items);
         }
        
      //  [HttpGet("/GetOverdue")]
      //  [ProducesResponseType((int)HttpStatusCode.OK)]
      //  public async Task<IActionResult> GetOverdue()
      //  {
      //      var borrowings = await _borrowingService.GetOverdueAsync();
      //      return Ok(borrowings);
      //  }



    }
}
