using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class ActivityRepository
    {
        public static int AddActivity(string activityType, string description,DateTime createdAt,string username,string entityName,int entityID)
        {
            string query = @"INSERT INTO Activities (ActivityType, Description,  CreatedAt, Username, EntityName, EntityID)
                               VALUES(@activityType, @description,  @createdAt, @username, @entityName, @entityID);
                               SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string, (SqlDbType, object, int?)>()
            {
                ["@activityType"] = (SqlDbType.NVarChar, activityType, 50),
                ["@description"] = (SqlDbType.NVarChar, description , 200),
                ["@createdAt"] = (SqlDbType.DateTime, createdAt, null),
                ["@username"] = (SqlDbType.NVarChar, username, 50),
                ["@entityName"] = (SqlDbType.NVarChar, entityName, 50),
                ["@entityID"] = (SqlDbType.Int, entityID, null),
            };
            object activityID = SqlHelper.ExecuteCommand(query,CommandType.Text,SqlHelper.ExecuteType.ExecuteScalar,parameters);
            return Convert.ToInt32(activityID);
        }
  
        public static List<ActivityDTO> GetAllActivities()
        {
            string query = @"SELECT Top 8 * FROM Activities Order by CreatedAt DESC;";
            List<ActivityDTO> activities = new();
            using var reader = SqlHelper.ExecuteReader(query,CommandType.Text,null);
            while (reader != null && reader.Read())
            {
                activities.Add(

                    new ActivityDTO
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ActivityType = reader["ActivityType"].ToString(),
                        Description = reader["Description"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Username = reader["Username"].ToString(),
                        EntityName = reader["EntityName"].ToString(),
                        EntityID = Convert.ToInt32(reader["EntityID"]),
                    }
                    );
            }
            return activities;
        }
    
    }
}
