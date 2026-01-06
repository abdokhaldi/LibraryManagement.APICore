using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.PersonDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpPost]
        [ProducesResponseType((int)StatusCodes.Status201Created)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> CreatePerson([FromBody] PersonForCreationDTO personDTO)
        {
            
            var newPersonID = await _personService.CreatePersonAsync(personDTO);
             
                return CreatedAtAction(nameof(GetPersonDetails), new {id = newPersonID}, newPersonID);
            }
        

        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPersonDetails(int id)
        {
            
            var person = await _personService.GetPersonDetailsAsync(id);
            if (person==null)
            {
                return NotFound($"The person with ID : {id} not found .");
            }
            return Ok(person);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePerson(int id, [FromBody]PersonForUpdateDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool updateResult = await _personService.UpdatePersonAsync(id,personDTO);
            if (updateResult == false)
            {
                return NotFound($"The person with ID:{id} not found for update .");
            }

            return NoContent();
        }
        [HttpPatch("{id}/ActivatePerson")]
        [ProducesResponseType((int) StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ActivatePerson(int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            bool activateResult = await _personService.ActivatePersonAsync(id);
            if (activateResult == false)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPatch("{id}/DeactivatePerson")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status204NoContent)]
        [ProducesResponseType((int) StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeactivatePerson(int id)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            bool deactivateResult = await _personService.DeactivatePersonAsync(id);
            if (deactivateResult == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        [ProducesResponseType((int)StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPersons()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _personService.GetAllPeopleAsync();
            return Ok(list);
        }
    }
}
