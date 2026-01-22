using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.AuthDTOs;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.RefreshTokenDTOs;
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



        [AllowAnonymous]
        [HttpPost("refresh_token")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO requestDTO)
        {
            var result = await _authService.RefreshTokenAsync(requestDTO);

            return result.status switch
            {

                LoginStatus.Success => Ok(result.Data),
                LoginStatus.Blocked => Unauthorized(new { Message = "User account is blocked." }),
                LoginStatus.Deactivated => Unauthorized(new { Message = "User account is deactivated." }),
                LoginStatus.InvalidCredentials => Unauthorized(new { Message = "Invalid or expired refresh token." }),
                _ => BadRequest()
            };
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> Logout([FromBody]LogoutRequestDTO requestDto)
        {
            var result = await _authService.LogoutAsync(requestDto);
            return result.status switch
            {

                LoginStatus.Success => Ok(new { Message= "Logged out successfully"}),
                LoginStatus.InvalidCredentials => BadRequest("Invalid or already revoked token."),
                _ => StatusCode(500, "An unexpected error occurred.")

            };

        }


    }
}
