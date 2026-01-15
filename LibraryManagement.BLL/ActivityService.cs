using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.ActivityDTOs;


namespace LibraryManagement.BLL
{
    
    public class ActivityService : IActivityService
    {

        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

      public async Task AddActivityAsync(ActivityForCreationDTO activityDTO)
        {
            
        }
        public async Task<List<ActivityForDisplayDTO>> GetQueryableAllActivitiesAsync()
        {
            return new List<ActivityForDisplayDTO>();
        }

    }
}
