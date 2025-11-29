using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class RoleRepository
    {
        public static List<RoleDTO> GetAllRoles()
        {
            List<RoleDTO> roles = null;
            string query = @"SELECT * FROM Roles;";
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,null);
            if (reader != null)
                roles = new List<RoleDTO>();
            while (reader.Read())
            {
                roles.Add(
                    new RoleDTO
                    {
                        RoleID = (int)reader["RoleID"],
                        RoleName = reader["RoleName"].ToString(),
                    }
                    );
            }
            return roles;
        }
        public static RoleDTO GetRoleByID(int ID)
        {
            string query = @"SELECT * FROM Roles WHERE RoleID=@ID;";
            var parameters =
                new Dictionary<string, (SqlDbType, object, int?)>()
                {
                    ["@ID"] = (SqlDbType.Int,ID,null),
                };
            var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
            if (reader.Read())
            {
                return new RoleDTO
                {
                    RoleID = Convert.ToInt32(reader["RoleID"]),
                    RoleName = reader["RoleName"].ToString()
                };
                
            }
            return null;
        }
    }
}
