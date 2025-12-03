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

        Task UpdatePersonAsync(Person personEntity);

    }
}