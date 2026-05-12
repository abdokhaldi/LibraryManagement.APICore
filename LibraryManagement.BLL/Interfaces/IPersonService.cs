using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IPersonService
    {
        Task<OperationResult<bool>> CheckPersonExistenceAsync(string nationalNumber);
        Task<OperationResult<PersonForDisplayDTO>> GetPersonDetailsAsync(Guid id);
        Task<PagedList<PersonForDisplayDTO>> GetAllPeopleAsync(PersonParameters parameters);
        Task<OperationResult> UpdatePersonAsync(Guid id, PersonForUpdateDTO personDTO);
        Task<OperationResult<Guid>> CreatePersonAsync(PersonForCreationDTO personDTO);
        Task<OperationResult> ActivatePersonAsync(Guid id);
        Task<OperationResult> DeactivatePersonAsync(Guid id);
        


    }
}
