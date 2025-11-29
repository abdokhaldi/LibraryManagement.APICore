using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class SmallPersonRepository
    {
        public static List<SmallPersonDTO> GetPersonAutoSearch(string searchTerm)
        {
            List<SmallPersonDTO> people = new List<SmallPersonDTO>();
            string query = @"SELECT * FROM SmallPeople WHERE FullName LIKE @searchTerm";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>
            {
                ["@searchTerm"] = (SqlDbType.NVarChar, searchTerm, null),
            };
            using var reader = SqlHelper.ExecuteReaderWildCard(query, CommandType.Text, parameters);
            if (reader == null)
            {
                people = null;
                return people;
            }
            while (reader.Read())
            {
                people.Add(
                    new SmallPersonDTO
                    {
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        FullName = reader["FullName"].ToString(),
                    });
                 }
            return people;
        }

    }
}
