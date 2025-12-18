using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.MemberDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace LibraryManagement.API.Controllers
{
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
        public async Task<IActionResult> GetMemberDetais(int id)
        {
            var member = await _memberService.GetMemberDetails(id);
            if (member == null)
            {
                return NotFound($"The book with ID:{id} not found .");
            }
            return Ok(member);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpPatch("{id}/ActivateMember")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
      public async Task<IActionResult> ActivateMember(int id)
        {
            bool activateResult = await _memberService.ActivateMember(id);
            if (activateResult==false)
            {
                return NotFound($"The member with ID :{id} not found to activate .");
            }
            
            return NoContent();
        }

        [HttpPatch("{id}/DeactivateMember")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
       public async Task<IActionResult> DeactivateMember(int id)
        {
            bool deactivateResult = await _memberService.DeactivateMember(id);

            if (deactivateResult == false)
            {
                return NotFound($"The member with ID :{id} not found to deactivate");
            }
            return NoContent();
        }

    }
}

