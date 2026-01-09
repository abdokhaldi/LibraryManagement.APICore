using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.PersonDTOs;

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
