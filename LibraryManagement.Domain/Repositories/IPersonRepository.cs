using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.Parameters;
using LibraryManagement.Shared.Helpers;
using System.Linq.Expressions;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<PagedList<Person>> GetActivePeopleAsync(PersonParameters parameters);
        Task<bool> CheckPersonExistenceAsync(Expression<Func<Person, bool>> predicate);
        Task<Person?> GetPersonForReadOnlyAsync(Guid personID);
        Task<Person?> GetPersonForUpdateAsync(Guid personID);
        Task<Person?> GetPersonAsync(Expression<Func<Person, bool>> predicate, bool isTracked=false);
        Task AddNewPersonAsync(Person personEntity);

        Task<(bool isNotFound,bool isNotActive)> CheckPersonStatus(Guid id);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsPhoneExists(string phone);
        Task<(bool EmailExists, bool PhoneExists)> IsEmailOrPhoneExistsAsync(string email, string phone);


    }
}