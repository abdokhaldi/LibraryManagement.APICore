
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories
{
    public interface IGlobalSettingsRepository
    {
        Task<GlobalSettings?> GetGlobalSettingsAsync(int id);
    }
}
