using LibraryManagement.BLL.Interfaces;
using LibraryManagement.BLL.Common;

using LibraryManagement.DTO.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO loginDTO)
        {
            var (status, message) = await _authService.LoginAsync(loginDTO.Identifier, loginDTO.Password);

            return status switch
            {
                LoginResult.Success => Ok(new { Message = message }),
                LoginResult.InvalidCredentials => Unauthorized(new { Message = message }),
                LoginResult.Blocked or LoginResult.Deactivated => StatusCode(StatusCodes.Status403Forbidden, new { Message = message }),
                _ => BadRequest()
            };
        }
    }
}
