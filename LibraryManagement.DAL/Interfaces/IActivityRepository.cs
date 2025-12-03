using System;
using LibraryManagement.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IActivityRepository
    {
        Task<int> AddActivityAsync(Activity activityEntity);
        Task<IQueryable<Activity>> GetQueryableAllActivitiesAsync();
    }
}
