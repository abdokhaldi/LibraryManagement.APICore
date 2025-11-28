using DTO_LibraryManagement;

using System;
using System.Collections.Generic;
using System.Data;


namespace DAL_LibraryManagement
{
    public class UserRepository
    {
        public static UserDTO GetUserByID(int ID)
        {
            string query = @"SELECT * FROM Users WHERE UserID=@ID;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@ID"] = (SqlDbType.Int,ID,null)
            };

           using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
            if (!reader.Read())
                return null;

            return new UserDTO
            {
                UserID = Convert.ToInt32(reader["UserID"]),
                PersonID = Convert.ToInt32(reader["PersonID"]),
                Username = reader["Username"].ToString(),
                RoleID = Convert.ToInt32(reader["RoleID"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
            };
        }
        public static UserDTO GetUserByUsername(string username)
        {
            string query = @"SELECT * FROM Users WHERE Username=@username;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@username"] = (SqlDbType.NVarChar, username, 50),
            };

           using  var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters);
            if (reader.Read())
            {

                return new UserDTO
                  {
                      UserID = (int)reader["UserID"],
                      PersonID = (int)reader["PersonID"],
                      Username = (string)reader["Username"],
                      Password = (string)reader["Password"],
                      RoleID = (int)reader["RoleID"],
                      IsActive = (bool)reader["IsActive"],
                      CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                };
            }
            return null;
        }

        public static List<UserDTO> GetAllUsers()
        {
            var users = new List<UserDTO>();

            string query = @"SELECT * FROM Users;";

          using var reader = SqlHelper.ExecuteReader(query,CommandType.Text);
            while (reader.Read())
            {
                users.Add(
                    new UserDTO
                    {
                        UserID = (int)reader["UserID"],
                        PersonID = (int)reader["PersonID"],
                        Username = (string)reader["Username"],
                        RoleID = (int)reader["RoleID"],
                        IsActive = (bool)reader["IsActive"],
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    }
                    );
            }
            return users;
        }

        public static object IsUsernameExists(string username)
        {
            string query = @"SELECT 1 FROM Users WHERE Username = @username;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@username"] = (SqlDbType.NVarChar,username,50)
            };
            var result = SqlHelper.ExecuteCommand(query,CommandType.Text, SqlHelper.ExecuteType.ExecuteScalar,parameters);
            return result;
        }

        public static int AddNewUser(UserDTO data)
        {
            string query = @"INSERT INTO Users (PersonID,Username,password,RoleID,IsActive,CreatedAt)
                             VALUES(@personID,@username,@password,@roleID,@isActive,@createdAt);
                             SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string , (SqlDbType,object,int?)>()
            {
                ["PersonID"]= (SqlDbType.Int,data.PersonID, null),
                ["Username"]= (SqlDbType.NVarChar,data.Username, 50),
                ["Password"]= (SqlDbType.NVarChar,data.Password, 200),
                ["RoleID"]  = (SqlDbType.Int,data.RoleID, null),
                ["IsActive"] = (SqlDbType.Bit, data.IsActive, null),
                ["CreatedAt"] = (SqlDbType.DateTime2, data.CreatedAt, null),

            };

            object newUserID = SqlHelper.ExecuteCommand(query,CommandType.Text, SqlHelper.ExecuteType.ExecuteScalar,parameters);
            return Convert.ToInt32(newUserID);
          }

        public static int UpdateUser(UserDTO data)
        {
            string query = @"Update Users 
                             SET PersonID = @personID ,
                                  Username = @username,
                                  Password = @password,
                                  RoleID = @roleID,
                                  IsActive = @isActive 
                                  WHERE UserID=@userID;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["UserID"] = (SqlDbType.Int, data.UserID, null),
                ["PersonID"] = (SqlDbType.Int, data.PersonID, null),
                ["Username"] = (SqlDbType.NVarChar, data.Username, 50),
                ["Password"] = (SqlDbType.NVarChar, data.Password, 150),
                ["RoleID"] = (SqlDbType.Int, data.RoleID, null),
                ["IsActive"] = (SqlDbType.Bit, data.IsActive, null),
            };
            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;
        }

        public static int SetUserActiveStatus(int userID,bool isActive)
        {
            string query = @"Update Users 
                           SET IsActive = @isActive
                           WHERE UserID=@userID;";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["UserID"] = (SqlDbType.Int, userID, null),
                ["IsActive"] = (SqlDbType.Bit, isActive, null),
            };

            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected;
        }
    }
}
