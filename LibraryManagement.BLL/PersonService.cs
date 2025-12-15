using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Entities;

using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using LibraryManagement.DTO.PersonDTOs;
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
        public async Task<int> UpdatePersonAsync(int id, PersonForUpdateDTO personDTO)
        {
            return 0;
        }

       public async Task<bool> ActivatePersonAsync(int id)
        {
            return false;
        }

        public async Task<bool> DeactivatePersonAsync(int id)
        {
            return false;
        }

      public async Task<List<Person>> GetAllPeopleAsync()
        {
            return new List<Person>();
        }

    }
}
