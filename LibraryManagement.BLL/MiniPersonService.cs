using DAL_LibraryManagement;
using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_LibraryManagement
{
    public class MiniPersonService
    {
        public int PersonID { get; set; }
        public string FullName { get; set; }

        public MiniPersonService(int personID,string fullName)
        {
            this.PersonID = personID;
            FullName = fullName;
        }


        public static List<MiniPersonService> GetPersonAutoSearch(string searchTerm)
        {
            List<MiniPersonService> people = null;
            var peopleList = SmallPersonRepository.GetPersonAutoSearch(searchTerm);
            if (peopleList == null)
                return null;
            people = peopleList.Select(p => new MiniPersonService(p.PersonID, p.FullName)).ToList();
            return people;
        }
    }
}
