using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.OperationResult;
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
        Task<OperationResult<PersonForDisplayDTO>> GetPersonDetailsAsync(int id);
        Task<List<PersonForDisplayDTO>> GetAllPeopleAsync();
        Task<OperationResult> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO);
        Task<OperationResult<int>> CreatePersonAsync(PersonForCreationDTO personDTO);
        Task<OperationResult> ActivatePersonAsync(int id);
        Task<OperationResult> DeactivatePersonAsync(int id);
        


    }
}
