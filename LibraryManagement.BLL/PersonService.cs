using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
namespace LibraryManagement.BLL
{
    
    public class PersonService
    {
        public enum Mode { AddNew=1,Update=2}
        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public char   Gender { get; set; }
        public bool   IsActive { get; set; }
        public string FullName
        {
            get
            {
                
                return FirstName + " " +  LastName;
            }
        }
        public int tempPersonID { get; set; }
        
        public  Mode _Mode =  Mode.AddNew;
        

        private void ApplyTempID()
        {
            if (tempPersonID > 0)
            {
                this.PersonID = tempPersonID;
            }
        }
        private PersonService(int personID ,string firstName, string lastName,  string phone, string email, string address, string city,char gender,bool isActive)
        {
            PersonID = personID;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
            _Mode = Mode.Update;
            City = city;
            Gender = gender;
            IsActive = isActive;
            ApplyTempID();
        }


        
        public PersonService()
        {
            PersonID = -1;
            FirstName = "";
            LastName = "";
            Phone = "";
            Email = "";
            Address = "";
            _Mode = Mode.AddNew;
            City = "";
            Gender = ' ';
            IsActive = true;
        }

        
        private PersonDTO _FillDTOToTransferData()
        {
            return new PersonDTO
            {
                PersonID = PersonID,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Email = Email,
                Address = Address ,
                City = City,
                Gender = Gender,
                IsActive = IsActive,
            };
        }


        private bool _AddNewPerson()
        {
            PersonDTO personData = _FillDTOToTransferData();
           this.PersonID = PersonRepository.AddNewPerson( personData );
           
           if( this.PersonID > 0)
            {
                ActivityRepository.AddActivity("Add", $"Person: {personData.FirstName + " " + personData.LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", PersonID);
                return true;
            }
            return false;
        }
        private bool _UpdatePersonByID()
        {
            PersonDTO personData = _FillDTOToTransferData();// fill the tdo object with current object values
            int rowsEffected = PersonRepository.UpdatePerson(personData);
           
            if (rowsEffected > 0)
            {
                ActivityRepository.AddActivity("Update", $"Update: {personData.FirstName + " " + personData.LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", PersonID);
                return true;
            }
            return false;
        }

        public OperationResultBLL Save()
        {
            switch (_Mode)
            {
                case Mode.AddNew:
                    if (_AddNewPerson())
                    {
                        ActivityRepository.AddActivity("Add", $"New person Added : {FirstName} {LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", PersonID);

                        _Mode = Mode.Update;
                        return OperationResultBLL.Ok("Person has been added successfully.");
                    }
                    else
                    {
                        return OperationResultBLL.Fail("Person cannot be added.");
                    }
                
                case Mode.Update:
                    if (_UpdatePersonByID())
                    {
                        ActivityRepository.AddActivity("Update", $"Person updated : {FirstName} {LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", PersonID);

                        return OperationResultBLL.Ok("Person has been Edited successfully!");
                    }
                    else
                    {
                        return OperationResultBLL.Fail("Person cannot be edited !");
                    }
              }
            return OperationResultBLL.Fail("An unexpected error occurred!");
        }

        public static List<PersonService> GetAllPeople()
        {
            var PeopleList = PersonRepository.GetAllPeople();
           
            if (PeopleList == null) return null;
           
           var people = PeopleList.Select(
               p => new PersonService(p.PersonID, p.FirstName, p.LastName, p.Phone, p.Email, p.Address, p.City, p.Gender, p.IsActive)
              ).ToList();

            return people;
        }
        public static PersonService FindPersonByID(int personID)
        {

          var personDTO = PersonRepository.FindPersonByID(personID);
           
            if (personDTO == null) return null;
            
                return new PersonService(personID, personDTO.FirstName, personDTO.LastName, personDTO.Phone, personDTO.Email, personDTO.Address, personDTO.City, personDTO.Gender, personDTO.IsActive);
        }
        
        private bool IsMember(int id)
        {
            MemberService member = MemberService.FindMemberByPersonID(id);
            return member != null;
        }

        
        public static OperationResultBLL DesactivePerson(PersonService person)
        {
            int rowsAffected = PersonRepository.SetPersonActiveStatus(person.PersonID, false);
            
                if (rowsAffected > 0)
                {
                    ActivityRepository.AddActivity("Deactive", $"Deactivated person: {person.FirstName} {person.LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", person.PersonID);
                return OperationResultBLL.Ok("Person Deactivated .");
                }

            return OperationResultBLL.Fail("Person cannot be deactivated .");
        }
        public static OperationResultBLL ActivatePerson(PersonService person)
        {
            int rowsAffected = PersonRepository.SetPersonActiveStatus(person.PersonID, true);
            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Reactive", $"Reactivated person : {person.FirstName} {person.LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", person.PersonID);
                return OperationResultBLL.Ok("Person Reactivated .");
            }

            return OperationResultBLL.Ok("Person cannot be Reactivated .");
         }

        public static OperationResultBLL DeletePerson(PersonService person)
        {
            int rowsAffected = PersonRepository.DeletePerson(person.PersonID);
            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Delete", $"Deleted person : {person.FirstName} {person.LastName}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Person", person.PersonID);
                return OperationResultBLL.Ok("Person was deleted successfully .");
            }
            return OperationResultBLL.Fail("person cannot be deleted");
        }


       
    }
}
