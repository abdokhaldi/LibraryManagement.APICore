using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.GlobalSettings;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.Common;
using AutoMapper;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using LibraryManagement.Domain.Entities;
namespace LibraryManagement.BLL
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public GlobalSettingsService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }private const string SettingsCacheKey = "GlobalSettings";

        public async Task<OperationResult<GlobalSettingsForReadOnlyDTO>> GetSettingsAsync()
            {
            if (!_cache.TryGetValue(SettingsCacheKey, out GlobalSettings? settings))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"--- [{DateTime.Now:T}] CACHE MISS: Fetching settings from Database... ---");
                Console.ResetColor();
                settings = await _unitOfWork.GlobalSettingsRepository.GetGlobalSettingsAsync(1);


                if (settings is not null)
                {
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(24))
                        .SetPriority(CacheItemPriority.NeverRemove);

                    _cache.Set(SettingsCacheKey, settings, options);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"--- [{DateTime.Now:T}] CACHE HIT: Returning settings from Memory... ---");
                Console.ResetColor();
            }

            if (settings is null)
                return OperationResult<GlobalSettingsForReadOnlyDTO>.Failure(OperationStatus.NotFound, "Settings not found");

            var settingsDTO = _mapper.Map<GlobalSettingsForReadOnlyDTO>(settings);

            return OperationResult<GlobalSettingsForReadOnlyDTO>.Success(settingsDTO);
        }


        public async Task<OperationResult> ChangeSettingsAsync(GlobalSettingsForUpdateDTO settings)
        {
            var settingsForChange = await _unitOfWork.GlobalSettingsRepository.GetGlobalSettingsAsync(1);
          
            if (settingsForChange is null)
                return OperationResult.Failure(OperationStatus.NotFound, "settings not found !");

            settingsForChange.LastUpdated = DateTime.UtcNow;
           
            _mapper.Map(settings, settingsForChange);
            

            await _unitOfWork.SaveChangesAsync();
            
            _cache.Remove(SettingsCacheKey);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"--- [{DateTime.Now:T}] CACHE INVALIDATED: Settings updated in DB and removed from Cache. ---");
            Console.ResetColor();

            return OperationResult.Success();
        }

    }
}
