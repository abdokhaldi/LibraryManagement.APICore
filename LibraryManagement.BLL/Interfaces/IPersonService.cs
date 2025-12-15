using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.PersonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<PersonForDisplayDTO?> GetPersonDetailsAsync(int id);
        Task<List<Person>> GetAllPeopleAsync();
        Task<int> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO);
        Task<int> CreatePersonAsync(PersonForCreationDTO personDTO);
        Task<bool> ActivatePersonAsync(int id);
        Task<bool> DeactivatePersonAsync(int id);

    }
}
