using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<IQueryable<Person>> GetQueryablePeopleAsync();

        Task<Person?> GetPersonForReadOnlyAsync(int personID);
        Task<Person?> GetPersonForUpdateAsync(int personID);

        Task AddNewPersonAsync(Person personEntity);

        Task<bool> IsPersonActive(int id);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsPhoneExists(string phone);
        Task<(bool EmailExists, bool PhoneExists)> IsEmailOrPhoneExistsAsync(string email, string phone);


    }
}