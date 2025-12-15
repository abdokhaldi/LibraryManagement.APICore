using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DTO.PersonDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newPerson = await _personService.CreatePersonAsync(personDTO);
            
                return CreatedAtAction(nameof(GetPersonDetails), new {id = newPerson}, newPerson);
            }
        

        [HttpGet("{id}")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonDetails(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _personService.GetPersonDetailsAsync(id);
            if (person==null)
            {
                return NotFound($"The person with ID : {id} not found .");
            }
            return Ok(person);
        }

    }
}
