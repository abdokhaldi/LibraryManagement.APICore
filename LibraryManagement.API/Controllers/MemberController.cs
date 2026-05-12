using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Shared.HEADER_KEYS;
using LibraryManagement.Shared.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace LibraryManagement.API.Controllers
{
    //[Authorize(Roles = "Admin,Librarian")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMemberDetails(int id)
        {
            var member = await _memberService.GetMemberDetails(id);
            if (member == null)
            {
                return NotFound($"The book with SettingsID:{id} not found .");
            }
            return Ok(member);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActiveMembers([FromQuery]MemberParameters parameters)
        {
            var pagedList = await _memberService.GetActiveMembersAsync(parameters);
            Response.Headers.Append(HeaderKeys.Pagination, JsonSerializer.Serialize(pagedList.Metadata));
            return Ok(pagedList.Items);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/ActivateMember")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
      public async Task<IActionResult> ActivateMember(int id)
        {
            bool activateResult = await _memberService.ActivateMember(id);
            if (activateResult==false)
            {
                return NotFound($"The member with SettingsID :{id} not found to activate .");
            }
            
            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("{id}/DeactivateMember")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
       public async Task<IActionResult> DeactivateMember(int id)
        {
            bool deactivateResult = await _memberService.DeactivateMember(id);

            if (deactivateResult == false)
            {
                return NotFound($"The member with SettingsID :{id} not found to deactivate");
            }
            return NoContent();
        }

        
    }
}

