using DAL_LibraryManagement;
using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_LibraryManagement
{
    public class ActivityService
    {
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string EntityName { get; set; }
        public int EntityID { get; set; }

        public ActivityService(int id, string activityType, string description, DateTime createdAt, string username, string entityName, int entityID)
        {
            Id = id;
            ActivityType = activityType;
            Description = description;
            CreatedAt = createdAt;
            Username = username;
            EntityName = entityName;
            EntityID = entityID;
        }

        public ActivityService()
        {
            Id = 0;
            ActivityType = "";
            Description = "";
            CreatedAt = DateTime.Now;
            Username = "";
            EntityName = "";
            EntityID = 0;
        }

        public ActivityService(ActivityDTO activity)
        {
            Id = activity.Id;
            ActivityType = activity.ActivityType;
            Description = activity.Description;
            CreatedAt = activity.CreatedAt;
            Username = activity.Username;
            EntityName = activity.EntityName;
            EntityID = activity.EntityID;
        }

        public static List<ActivityService> GetAllActivities()
        {
            var list = ActivityRepository.GetAllActivities();
           
            return list.Select(
                 a => new ActivityService(a)
                ).ToList();
        }



    }
}
