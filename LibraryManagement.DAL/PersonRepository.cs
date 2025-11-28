using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DTO_LibraryManagement;
namespace DAL_LibraryManagement
{
    public class PersonRepository
    {
         public static List<PersonDTO> GetAllPeople()
        {
            var listOfPeople = new List<PersonDTO>();

            string query = $"SELECT * FROM People"; // SQL query
           using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,null);
            
            if (reader == null) return null;
            
            while (reader.Read())
            {
                listOfPeople.Add(
                    new PersonDTO
                    {
                        PersonID = Convert.ToInt32(reader["PersonID"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        Phone = Convert.ToString(reader["Phone"]),
                        Email = Convert.ToString(reader["Email"]),
                        Address = Convert.ToString(reader["Address"]),
                        City = reader["City"].ToString(),
                        Gender = Convert.ToChar(reader["Gender"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                    }
                    );
            }
            return listOfPeople;
        }
        public static PersonDTO FindPersonByID(int id)
        {
            string query = @"SELECT * FROM People WHERE PersonID=@personID";


            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        comm.Parameters.Add("@personID", SqlDbType.Int).Value = id;

                        SqlDataReader reader = comm.ExecuteReader();
                        if (reader.Read())
                        {
                            return new PersonDTO {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Address = reader["Address"].ToString(),
                                Email = reader["Email"].ToString(),
                                City = reader["City"].ToString(),
                                Gender = Convert.ToChar(reader["Gender"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                            };
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                
                throw new Exception($"Error : {ex.Message}");
                
            }
            return null;
        }
        public static int AddNewPerson(PersonDTO data)
        {

            string query = @"INSERT INTO People (FirstName,LastName,Phone,Email,Address,City,Gender,IsActive)
                           VALUES(@firstName,@lastName,@phone,@email,@address,@city,@gender,@isActive)
                           SELECT CAST(SCOPE_IDENTITY() AS int);";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@firstName"] = (SqlDbType.NVarChar,data.FirstName ,50),
                ["@lastName"] = (SqlDbType.NVarChar,data.LastName, 50),
                ["@phone"] = (SqlDbType.NVarChar,data.Phone, 20),
                ["@email"] = (SqlDbType.NVarChar,data.Email, 100),
                ["@address"] = (SqlDbType.NVarChar,data.Address, 200),
                ["@city"] = (SqlDbType.NVarChar, data.City, 20),
                ["@gender"] = (SqlDbType.NChar, data.Gender, null),
                ["@isActive"] = (SqlDbType.Bit, data.IsActive,null),

            };
            object newID = SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteScalar,parameters);
           return Convert.ToInt32( newID);
        }

       


        public static int UpdatePersonByID(PersonDTO data)
        {
            string query = @"UPDATE People 
                              SET FirstName= @firstName,
                                  LastName=@lastName,
                                  Phone=@phone,
                                  Email=@email,
                                  Address=@address,
                                  City = @city,
                                  Gender = @gender,
                                  IsActive = @isActive
                              WHERE PersonID=@personID;";
            int rowsAffected = 0;
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@personID"] = (SqlDbType.Int, data.PersonID,null),
                ["@firstName"] = (SqlDbType.NVarChar, data.FirstName, 50),
                ["@lastName"] = (SqlDbType.NVarChar, data.LastName, 50),
                ["@phone"] = (SqlDbType.NVarChar, data.Phone, 20),
                ["@email"] = (SqlDbType.NVarChar, data.Email, 100),
                ["@address"] = (SqlDbType.NVarChar, data.Address, 200),
                ["@city"] = (SqlDbType.NVarChar, data.City, 20),
                ["@gender"] = (SqlDbType.NChar, data.Gender, null),
                ["@isActive"] = (SqlDbType.Bit, data.IsActive, null),
            };
            rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;
        }
        
        public static int SetPersonActiveStatus(int personID,bool isActive)
        {
            string query = @"UPDATE People 
                             SET IsActive = @isActive WHERE PersonID=@personID";
            
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@personID"] = (SqlDbType.Int, personID , null),
                ["@isActive"] = (SqlDbType.Bit, isActive,null),
            };

            int rowsAffected = (int) SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected ;
        }

        public static int DeletePerson(int personID)
        {
            string query = @"DELETE FROM People WHERE PersonID = @personID;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@PersonID"] = (SqlDbType.Int,personID,null),
            };

            object rowsAffected = SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return Convert.ToInt32(rowsAffected);
        }
        
    }
}
