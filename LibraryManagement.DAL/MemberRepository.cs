
using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL_LibraryManagement
{
    public class MemberRepository
    {
        public static List<MemberDTO> GetAllMembers()
        {
            var members = new List<MemberDTO>() ;
            MemberDTO data = null;
            string query = @"SELECT * FROM Members;";
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text);
           while (reader.Read())
            {
               data = new MemberDTO
                {
                    MemberID = Convert.ToInt32(reader["PersonID"]),
                    PersonID = Convert.ToInt32(reader["PersonID"]),
                    JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
               };
                members.Add(data);
            }
            return members;
            }
       
        public static int AddNewMember(int personID,DateTime joinDate,bool isActive)
        {

            string query = @"INSERT INTO Members(PersonID,JoinDate,IsActive)
                             VALUES(@personID,@joinDate,@isActive);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";
            try
            {
                var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
                {
                    ["@personID"] = (SqlDbType.Int,personID,null) ,
                    ["@joinDate"] = (SqlDbType.DateTime,joinDate,null),
                    ["@isActive"] = (SqlDbType.Bit, isActive, null)
                };

                object newMemberID = SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteScalar,parameters);
                return Convert.ToInt32(newMemberID);
            }
            catch (Exception ex){
                throw new Exception($"Error : {ex.Message}");
            }
            
        }
       
        public static MemberDTO FindMemberByID(int id)
        {
           
            string query = @"SELECT * FROM Members WHERE PersonID=@id;";
            var parameters = new Dictionary<string, (SqlDbType, object,int?)>
            {
                ["@id"] = (SqlDbType.Int,id,null)
            };
           
           using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
            if (!reader.Read()) return null;
            
                return new MemberDTO
                {
                    MemberID = Convert.ToInt32(reader["PersonID"]),
                    PersonID = Convert.ToInt32(reader["PersonID"]),
                    JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                };
            }
    
        public static bool UpdateMember(MemberDTO data)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Members
                             SET PersonID = @personID,
                                 JoinDate = @joinDate
                             WHERE PersonID = @memberID;";

            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@memberID"] = (SqlDbType.Int, data.MemberID, null),
                ["@personID"] = (SqlDbType.Int, data.PersonID, null),
                ["@joinDate"] = (SqlDbType.DateTime, data.JoinDate, null),
                ["@isActive"] = (SqlDbType.Bit, data.IsActive, null)
            };
          
            try
            {
             rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}"); 
            }
            return rowsAffected > 0;
        }
    
        public static MemberDTO FindMemberByPersonID(int id)
        {
            string query = @"SELECT * FROM Members WHERE PersonID=@id;";
            var parameters = new Dictionary<string,(SqlDbType,object,int?)>()
            {
                ["@id"] = (SqlDbType.Int,id,null)
            };

            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,parameters);
            if (!reader.Read()) return null;
            
                return new MemberDTO
                {
                    MemberID = Convert.ToInt32(reader["PersonID"]),
                    PersonID = Convert.ToInt32(reader["PersonID"]),
                    JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),

                };
            }

        public static int SetMemberActiveStatus(int memberID,bool isActive)
        {
            string query = @"UPDATE Members 
                       SET IsActive=@isActive 
                           WHERE PersonID=@memberID;";

            var parameters = new Dictionary<string,(SqlDbType,object,int?)> {

                ["@memberID"] = (SqlDbType.Int,memberID,null),
                ["@isActive"] = (SqlDbType.Bit, isActive, null)
            };
            int rowsAffected = (int)SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteNonQuery,parameters);
            return rowsAffected ;
        }


    }
}
