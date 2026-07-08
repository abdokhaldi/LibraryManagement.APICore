using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LibraryManagement.API.Common;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.HEADER_KEYS;
using System.Text.Json;



namespace LibraryManagement.API.Controllers
{

    [Authorize(Roles ="Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDTO userDTO)
        {
            var result = await _userService.RegisterUserAsync(userDTO , CurrentUserRole);

            if (result.IsSuccess) 
                return CreatedAtAction(nameof(GetUserDetails), new { id = result.Data }, result.Data);
            return HandleErrorResult(result);
        }
         
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserDetails(Guid id)
        {
            var result = await _userService.GetUserDetailsAsync(id);
            
               if(result.IsSuccess)
                return  Ok(result.Data);
            return HandleErrorResult(result);
            
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDTO userDTO)
        {
            var result = await _userService.UpdateUserAsync(CurrentUserRole, id, userDTO);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActiveUsers([FromQuery] UserParameters parameters)
        {
            var usersPaged = await _userService.GetActiveUsersAsync(parameters);

            Response.Headers.Append(HeaderKeys.Pagination, JsonSerializer.Serialize(usersPaged.Metadata));
           
            return Ok(usersPaged.Items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            var result = await _userService.DeactivateUserAsync(id);

            if (result.IsSuccess) 
                return NoContent();
                return HandleErrorResult(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/activate")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ActiveUser(Guid id)
        {
            var result = await _userService.ActivateUserAsync(id);
            if (result.IsSuccess) 
                return NoContent();
            return HandleErrorResult(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/block")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            var result = await _userService.BlockUserAsync(id);
            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/unblock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnblockUser(Guid id)
        {
            var result = await _userService.UnblockUserAsync(id);
            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

        
    }
}
