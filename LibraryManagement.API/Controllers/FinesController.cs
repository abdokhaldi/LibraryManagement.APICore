using Microsoft.AspNetCore.Mvc;
using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.HEADER_KEYS;
using System.Text.Json;
using System.Net;
namespace LibraryManagement.API.Controllers

{

    [ApiController]
    [Route("api/[controller]")]

    public class FinesController : BaseController
    {
        private readonly IFineService _fineService;

        public FinesController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetFines([FromQuery] FineParameters parameters)
        {
            var pagedList = await _fineService.GetFinesAsync(parameters);

            Response.Headers.Append(HeaderKeys.Pagination, JsonSerializer.Serialize(pagedList.Metadata));
            return Ok(pagedList.Items);
        }

        [HttpPatch("{id}/Pay")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Pay(int id)
        {
            var result = await _fineService.PayAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/Waive")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Waive(int id, string waiveReason)
        {
            var result = await _fineService.WaiveAsync(id, waiveReason);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

    }
}
