using LibraryManagement.API.Common;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.PersonDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryManagement.API.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> CreatePerson([FromBody] PersonForCreationDTO personDTO)
        {
            
            var result = await _personService.CreatePersonAsync(personDTO);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetPersonDetails), new { id = result.Data }, result.Data);
                       return HandleErrorResult(result);
      }
        

        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPersonDetails(int id)
        {
            
            var result = await _personService.GetPersonDetailsAsync(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return HandleErrorResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]

        public async Task<IActionResult> UpdatePerson(int id, [FromBody]PersonForUpdateDTO personDTO)
        {
            
            var result = await _personService.UpdatePersonAsync(id,personDTO);

            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/ActivatePerson")]
        [ProducesResponseType((int) StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ActivatePerson(int id)
        {
           
            var result = await _personService.ActivatePersonAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/DeactivatePerson")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeactivatePerson(int id)
        {
            
            var result = await _personService.DeactivatePersonAsync(id);
            if (result.IsSuccess)
                return NoContent();

            return HandleErrorResult(result);
        }

        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPersons()
        {
            
            var persons = await _personService.GetAllPeopleAsync();
            return Ok(persons);
        }
    }
}
