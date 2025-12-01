using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Entities;
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
    public class ActivityRepository
    {
        private readonly LibraryDbContext _context;
        public ActivityRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddActivityAsync(Activity activityEntity)
        {
            try
            {
                _context.Activities.Add(activityEntity);
                await _context.SaveChangesAsync();
                return activityEntity.ActivityID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
  
        public async Task<List<Activity>> GetAllActivitiesAsync()
        {
            try
            {
                var activitiesList = await _context.Activities.Take(8)
                                                              .ToListAsync();
                return activitiesList;
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    
    }
}
