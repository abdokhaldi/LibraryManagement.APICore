using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO.Identifier, loginDTO.Password);

            
            return result.status switch
            {
                LoginStatus.Success => Ok(result),
                LoginStatus.InvalidCredentials => Unauthorized("Invalid username or password ."),
                LoginStatus.Blocked  => StatusCode(StatusCodes.Status403Forbidden, new { Message = "You was blocked, contact the admin" }),
                LoginStatus.Deactivated => StatusCode(StatusCodes.Status403Forbidden, new { Message = "You was inactivated, contact the admin" }),
                _ => BadRequest()
            };
        }
    }
}
