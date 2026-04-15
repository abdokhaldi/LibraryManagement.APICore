using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.GlobalSettings;
using LibraryManagement.API.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : BaseController
    {
        private readonly IGlobalSettingsService _globalSettingsService;
        public SettingsController(IGlobalSettingsService globalSettingsService)
        {
           _globalSettingsService = globalSettingsService;
        }


        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangeSettings(GlobalSettingsForUpdateDTO settings)
        {
            var result = await _globalSettingsService.ChangeSettingsAsync(settings);

            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetSettings()
        {
            var result = await _globalSettingsService.GetSettingsAsync();

            if (result.IsSuccess)
                return Ok(result.Data);

            return HandleErrorResult(result);
        }

    }
}
