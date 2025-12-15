using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Entities;

using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using LibraryManagement.DTO.PersonDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
namespace LibraryManagement.BLL
{
    
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreatePersonAsync(PersonForCreationDTO personDTO)
        {
            var personEntity = _mapper.Map<Person>(personDTO);
            personEntity.IsActive = true;
            await _unitOfWork.PersonRepository.AddNewPersonAsync(personEntity);
            await _unitOfWork.SaveChangesAsync();
            return personEntity.PersonID;
        }

        public async Task<PersonForDisplayDTO?> GetPersonDetailsAsync(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetPersonForReadOnlyAsync(id);
            if (person == null)
            {
                return null;
            }
            var personDTO = _mapper.Map<PersonForDisplayDTO>(person);
            return personDTO;
        }
        public async Task<bool> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO)
        {
            var personForUpdate = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (personForUpdate == null || personForUpdate.IsActive==false)
            {
                return false;
            }
            
             _mapper.Map(personDTO,personForUpdate);
            
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

       public async Task<bool> ActivatePersonAsync(int id)
        {
            var personForActivate = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (personForActivate == null)
            {
                return false;
            }
            if (personForActivate.IsActive == true)
            {
                return true;
            }
            personForActivate.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivatePersonAsync(int id)
        {
            var personForActivate = await _unitOfWork.PersonRepository.GetPersonForUpdateAsync(id);
            if (personForActivate == null)
            {
                return false;
            }
            if (personForActivate.IsActive == false)
            {
                return false;
            }
            personForActivate.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

      public async Task<List<PersonForDisplayDTO>> GetAllPeopleAsync()
        {
            var personsQuery = await _unitOfWork.PersonRepository.GetQueryablePeopleAsync();
            var activePersons = await personsQuery
                                     .Where(p => p.IsActive == true)
                                     .ProjectTo<PersonForDisplayDTO>(_mapper.ConfigurationProvider)
                                     .ToListAsync();
            return activePersons;                                                  
         }

    }
}
