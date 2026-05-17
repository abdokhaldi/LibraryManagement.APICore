using AutoMapper;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.TenantDTOs;
using LibraryManagement.BLL.Interfaces;


namespace LibraryManagement.BLL
{
    public  class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TenantService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
   
    public async Task<OperationResult<TenantForDisplayDTO>> GetTenantAsync(Guid id)
        {
            var tenant = await _unitOfWork.TenantRepository.GetTenantAsync(t => t.TenantID == id );
            if (tenant == null)
                return OperationResult<TenantForDisplayDTO>.Failure(OperationStatus.NotFound, "Tenant not found" );

            var mappedTenant = _mapper.Map<TenantForDisplayDTO>(tenant);

            return OperationResult<TenantForDisplayDTO>.Success(mappedTenant);
     }

        public async Task<OperationResult> UpdateTenantAsync(Guid id , TenantForUpdateDTO tenant)
        {
            var tenantForUpdate = await _unitOfWork.TenantRepository.GetTenantAsync(t => t.TenantID == id, tracking: true);
            if (tenantForUpdate == null)
                return OperationResult.Failure(OperationStatus.NotFound, "Tenant not found");
            if (!tenantForUpdate.IsActive)
                return OperationResult.Failure(OperationStatus.NotFound, "Tenant not found");

             _mapper.Map(tenant, tenantForUpdate);

            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success();

        }
    }
}
