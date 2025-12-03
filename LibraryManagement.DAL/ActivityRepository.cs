using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
