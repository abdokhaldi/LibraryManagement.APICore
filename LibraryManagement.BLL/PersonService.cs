using System;
using System.Collections.Generic;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.Common;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;

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

        public async Task<OperationResult<int>> CreatePersonAsync(PersonForCreationDTO personDTO)
        {
            var check = await _unitOfWork.PersonRepository.IsEmailOrPhoneExistsAsync(personDTO.Email,personDTO.Phone);
            if (check.EmailExists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict,"Email is already existing.");
            }
            if (check.PhoneExists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict, "Phone number is already existing.");
            }
            var personEntity = _mapper.Map<Person>(personDTO);
            personEntity.IsActive = true;

            await _unitOfWork.PersonRepository.AddNewPersonAsync(personEntity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<int>.Success(personEntity.PersonID);
        }

        public async Task<OperationResult<PersonForDisplayDTO>> GetPersonDetailsAsync(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForReadOnlyAsync(id);
            if (person == null)
            {
                return OperationResult<PersonForDisplayDTO>.Failure(OperationStatus.NotFound, $"The person with ID: {id} was not found.");
            }

            var personDTO = _mapper.Map<PersonForDisplayDTO>(person);
            return OperationResult<PersonForDisplayDTO>.Success(personDTO);
        }

        public async Task<OperationResult> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO)
        {
            var personForUpdate = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);

            if (personForUpdate == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"The person with ID: {id} was not found for update.");
            }

            if (!personForUpdate.IsActive)
            {
                return OperationResult.Failure(OperationStatus.Conflict, "Cannot update an inactive person.");
            }

            _mapper.Map(personDTO, personForUpdate);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();
        }

        public async Task<OperationResult> ActivatePersonAsync(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (person == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"Person with ID: {id} not found.");
            }

            if (person.IsActive) return OperationResult.Success();

            person.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> DeactivatePersonAsync(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (person == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, $"Person with ID: {id} not found.");
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