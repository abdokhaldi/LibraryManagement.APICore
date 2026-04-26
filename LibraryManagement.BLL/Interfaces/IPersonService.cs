using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<OperationResult<bool>> CheckPersonExistenceAsync(string nationalNumber);
        Task<OperationResult<PersonForDisplayDTO>> GetPersonDetailsAsync(int id);
        Task<PagedList<PersonForDisplayDTO>> GetAllPeopleAsync(PersonParameters parameters);
        Task<OperationResult> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO);
        Task<OperationResult<int>> CreatePersonAsync(PersonForCreationDTO personDTO);
        Task<OperationResult> ActivatePersonAsync(int id);
        Task<OperationResult> DeactivatePersonAsync(int id);
        


    }
}
