using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.GlobalSettings;
using LibraryManagement.DTO.OperationResults;


namespace LibraryManagement.BLL.Interfaces
{
    public interface IGlobalSettingsService
    {
        Task<OperationResult<GlobalSettingsForReadOnlyDTO>> GetSettingsAsync();
        Task<OperationResult> ChangeSettingsAsync(GlobalSettingsForUpdateDTO settings);
    }
}
