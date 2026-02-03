using Microsoft.AspNetCore.Mvc;
using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.HEADER_KEYS;
using System.Text.Json;
using LibraryManagement.DTO.BookCopyDTO;

namespace LibraryManagement.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopyController : BaseController
    {
        private readonly IBookCopyService _bookCopyService;

        public BookCopyController(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
       public async Task<IActionResult> GetBookCopy(int id)
        {
            var result = await _bookCopyService.GetBookCopyAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return HandleErrorResult(result);
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetBookCopies([FromQuery] BookCopyParameters parameters)
        {
            var pagedCopies = await _bookCopyService.GetCopiesAsync(parameters);

            Response.Headers.Append(HeaderKeys.Pagination,JsonSerializer.Serialize(pagedCopies.Metadata));

            return Ok(pagedCopies.Items);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCopy([FromBody] BookCopyForCreationDTO copyDTO)
        {
            var result = await _bookCopyService.CreateCopyAsync(copyDTO);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetBookCopy), new {id=result.Data }, result.Data);
            return HandleErrorResult(result);

        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCopy(int id ,[FromBody] BookCopyForUpdateDTO copyDTO)
        {
            var result = await _bookCopyService.UpdateCopyAsync(id, copyDTO);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/ActivateCopy")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ActivateCopy(int id)
        {
            var result = await _bookCopyService.ActivateCopyAsync(id);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/DeactivateCopy")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeactivateCopy(int id)
        {
            var result = await _bookCopyService.DeactivateCopyAsync(id);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }
    }
}
