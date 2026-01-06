using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using LibraryManagement.DTO.Common;
using LibraryManagement.API.Common;
using LibraryManagement.DTO.OperationResult;



namespace LibraryManagement.API.Controllers
{
    //[Authorize(Roles ="Admin")]
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
            var result = await _userService.RegisterUserAsync(userDTO);

            if (result.IsSuccess) 
                return CreatedAtAction(nameof(GetUserDetails), new { id = result.Data }, result.Data);
            return HandleErrorResult(result);
        }
         
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserDetails(int id)
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDTO userDTO)
        {
            var result = await _userService.UpdateUserAsync(id, userDTO);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

       
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllActiveUsersAsync();
            return Ok(users);
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> DeactivateUser(int id)
        {
            var result = await _userService.DeactivateUserAsync(id);

            if (result.IsSuccess) 
                return NoContent();
                return HandleErrorResult(result);

        }
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActiveUser(int id)
        {
            var result = await _userService.ActivateUserAsync(id);
            if (result.IsSuccess) 
                return NoContent();
            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/block")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BlockUser(int id)
        {
            var result = await _userService.BlockUserAsync(id);
            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

        [HttpPatch("{id}/unblock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var result = await _userService.UnblockUserAsync(id);
            if (result.IsSuccess)
                return NoContent();
            return HandleErrorResult(result);
        }

        
    }
}
