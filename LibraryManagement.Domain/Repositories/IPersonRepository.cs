using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<IQueryable<Person>> GetQueryablePeopleAsync();

        Task<Person?> GetPersonForReadOnlyAsync(int personID);
        Task<Person?> GetPersonForUpdateAsync(int personID);

        Task AddNewPersonAsync(Person personEntity);

        Task<(bool isNotFound,bool isNotActive)> CheckPersonStatus(int id);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsPhoneExists(string phone);
        Task<(bool EmailExists, bool PhoneExists)> IsEmailOrPhoneExistsAsync(string email, string phone);


    }
}