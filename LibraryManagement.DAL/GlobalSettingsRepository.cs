
using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryManagement.DAL
{
    public class GlobalSettingsRepository : IGlobalSettingsRepository
    {
        private readonly LibraryDbContext _context;
       
        
        public GlobalSettingsRepository(LibraryDbContext context)
        {
            _context = context;
           
        }

        // this function will get a traked object for update
        public async Task<GlobalSettings?> GetGlobalSettingsAsync( int id)
        {
              var  settings = await _context.GlobalSettings.FindAsync(id);
              return settings;
        }
    }
}
