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
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> RecordNewBorrowing([FromBody] BorrowingForCreationDTO borrowingDTO)
        {
            int newBorrowingID = await _borrowingService.CreateBorrowingAsync(borrowingDTO);
            if (newBorrowingID == 0)
            {
                return BadRequest("No book to borrow");
            }
            return CreatedAtAction(nameof(GetBorrowingDetails),new {id=newBorrowingID },newBorrowingID);
        }
       
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBorrowingDetails(int id)
        {
            return Ok($"{id}");
        }



    }
}
