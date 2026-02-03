using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace LibraryManagement.DAL
{
    public class ActivityRepository : IActivityRepository

    {
        private readonly LibraryDbContext _context;
        public ActivityRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public Task AddActivityAsync(Activity activityEntity)
        {
            
               _context.Activities.Add(activityEntity);
            return Task.CompletedTask;
            }
            
  
        public  Task<IQueryable<Activity>> GetQueryableAllActivitiesAsync()
        {
            
                var query = _context.Activities.AsNoTracking();
                                                             
                return Task.FromResult(query);
            }
            
    
    }
}
