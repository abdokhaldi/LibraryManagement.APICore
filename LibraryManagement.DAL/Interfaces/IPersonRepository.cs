using LibraryManagement.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<IQueryable<Person>> GetQueryablePeopleAsync();

        Task<Person?> FindPersonByIDAsync(int personID);

        Task<int> AddNewPersonAsync(Person personEntity);

        Task<int> UpdatePersonAsync(Person personEntity);

        Task<int> SetPersonStatusAsync(int personID, bool isActive);
    }
}