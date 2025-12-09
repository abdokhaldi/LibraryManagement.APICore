using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using LibraryManagement.DTO.ActivityDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    
    public class ActivityService : IActivityService
    {

        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
    }
}
