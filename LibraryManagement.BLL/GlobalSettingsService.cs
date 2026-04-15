using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.GlobalSettings;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.Common;
using AutoMapper;

namespace LibraryManagement.BLL
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GlobalSettingsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

            public async Task<OperationResult<GlobalSettingsForReadOnlyDTO>> GetSettingsAsync()
            {
                var settings = await _unitOfWork.GlobalSettingsRepository.GetGlobalSettingsAsync(1);
              
            if (settings is null)
                    return OperationResult<GlobalSettingsForReadOnlyDTO>.Failure(OperationStatus.NotFound ,"Settings not found");

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

            return OperationResult.Success();
        }

    }
}
