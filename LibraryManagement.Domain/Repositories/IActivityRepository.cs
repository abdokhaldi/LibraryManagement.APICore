using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Domain.Interfaces
{
    public interface IActivityRepository
    {
        Task AddActivityAsync(Activity activityEntity);
        Task<IQueryable<Activity>> GetQueryableAllActivitiesAsync();
    }
}
