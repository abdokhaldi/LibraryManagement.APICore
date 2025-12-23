using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
            int newUserID = await _userService.RegisterUserAsync(userDTO);
            if (newUserID == -1)
            {
                return BadRequest("This username is already Used , try another one .");
            }
            return CreatedAtAction(nameof(GetUserDetails), new {id=newUserID }, newUserID);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var user = await _userService.GetUserDerailsAsync(id);
            if (user == null)
            {
                return NotFound($"The user with ID: {id} is not found");
            }
            return Ok(user);
        }
        }
}
