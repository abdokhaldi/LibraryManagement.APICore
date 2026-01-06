using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.RoleDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] RoleForCreationDTO roleDTO)
        {
            int newRoleID = await _roleService.CreateRoleAsync(roleDTO);
            if (newRoleID == -1)
            {
                return BadRequest($"The role is already existing .");
            }
            return CreatedAtAction(nameof(GetRole),new { id = newRoleID },newRoleID );
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleService.GetRoleAsync(id);
            if (role == null)
            {
                return NotFound($"The role with ID:{id} not found");
            }
            return Ok(role);
        }


    }
}
