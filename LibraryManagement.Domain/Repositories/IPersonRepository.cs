using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Parameters;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetQueryablePeople(PersonParameters parameters);

        Task<Person?> GetPersonForReadOnlyAsync(int personID);
        Task<Person?> GetPersonForUpdateAsync(int personID);

        Task AddNewPersonAsync(Person personEntity);

        Task<(bool isNotFound,bool isNotActive)> CheckPersonStatus(int id);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsPhoneExists(string phone);
        Task<(bool EmailExists, bool PhoneExists)> IsEmailOrPhoneExistsAsync(string email, string phone);


    }
}