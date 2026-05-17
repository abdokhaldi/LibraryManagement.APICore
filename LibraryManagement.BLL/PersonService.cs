
using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.BLL
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<Guid>> CreatePersonAsync(PersonForCreationDTO personDTO)
        {
            var check = await _unitOfWork.PersonRepository.IsEmailOrPhoneExistsAsync(personDTO.Email,personDTO.Phone);
            if (check.EmailExists)
            {
                return OperationResult<Guid>.Failure(OperationStatus.Conflict,"Email is already existing.");
            }
            if (check.PhoneExists)
            {
                return OperationResult<Guid>.Failure(OperationStatus.Conflict, "Phone number is already existing.");
            }
            var personEntity = _mapper.Map<Person>(personDTO);
            personEntity.PersonID = Guid.NewGuid();
            personEntity.IsActive = true;

            await _unitOfWork.PersonRepository.AddNewPersonAsync(personEntity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<Guid>.Success(personEntity.PersonID);
        }

        public async Task<OperationResult<bool>> CheckPersonExistenceAsync(string nationalNumber)
        {
            bool isFound = await _unitOfWork.PersonRepository.CheckPersonExistenceAsync(p => p.NationalNumber == nationalNumber);
            if (!isFound)
                return OperationResult<bool>.Failure(OperationStatus.NotFound, "Not found");
          
            return OperationResult<bool>.Success(true);

        }

        public async Task<OperationResult<PersonForDisplayDTO>> GetPersonDetailsAsync(Guid id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForReadOnlyAsync(id);
            if (person == null)
            {
                return OperationResult<PersonForDisplayDTO>.Failure(OperationStatus.NotFound, $"The person with personID: {id} was not found.");
            }

            var personDTO = _mapper.Map<PersonForDisplayDTO>(person);
            return OperationResult<PersonForDisplayDTO>.Success(personDTO);
        }

        public async Task<OperationResult> UpdatePersonAsync(Guid id, PersonForUpdateDTO personDTO)
        {
            var personForUpdate = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);

            if (personForUpdate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The person with SettingsID: {id} was not found for update.");
            }

            if (!personForUpdate.IsActive)
            {
                return OperationResult.Failure(OperationStatus.Conflict, "Cannot update an inactive person.");
            }

            _mapper.Map(personDTO, personForUpdate);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<OperationResult> ActivatePersonAsync(Guid id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (person == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"Person with SettingsID: {id} not found.");
            }

            if (person.IsActive) return OperationResult.Success();

            person.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> DeactivatePersonAsync(Guid id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (person == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"Person with SettingsID: {id} not found.");
            }

            if (!person.IsActive) return OperationResult.Success();

            person.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<PagedList<PersonForDisplayDTO>> GetAllPeopleAsync(PersonParameters parameters)
        {
            var pagedPersons = await _unitOfWork.PersonRepository.GetActivePeopleAsync(parameters);

            var personsDTO = _mapper.Map<List<PersonForDisplayDTO>>(pagedPersons.Items);

            return  pagedPersons.MapTo(personsDTO);
        }
    }
}