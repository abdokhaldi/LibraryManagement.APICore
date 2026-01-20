using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.RoleDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Authorize(Roles ="Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] RoleForCreationDTO roleDTO)
        {
            var result = await _roleService.CreateRoleAsync(roleDTO);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetRole), new { id = result.Data }, result.Data);
                       return HandleErrorResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRole(int id)
        {
            var result = await _roleService.GetRoleAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return HandleErrorResult(result);
        }


    }
}
