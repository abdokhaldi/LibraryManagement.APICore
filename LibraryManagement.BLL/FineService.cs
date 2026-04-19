using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.FineDTO;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.Shared.Helpers;
using LibraryManagement.Shared.Parameters;


namespace LibraryManagement.BLL
{
    public class FineService : IFineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult> WaiveAsync(int id, string waiveReason) {
            var fineToWaive = await _unitOfWork.FineRepository.GetFineByIdAsync(id);
           if (fineToWaive == null) {
                return OperationResult.Failure(OperationStatus.NotFound, "Fine not found");
           }
            if (fineToWaive.PaidAt != null || fineToWaive.Status.Equals("Waived", StringComparison.OrdinalIgnoreCase))
            {
                return OperationResult.Failure(OperationStatus.Conflict, "Fine was already completed (Paid/Waived");
            }

            fineToWaive.Status = "Waived";
           fineToWaive.WaiveReason = waiveReason;

            await _unitOfWork.SaveChangesAsync();

           return OperationResult.Success("Fine waived successfully");
        }
        public async Task<OperationResult> PayAsync(int id) {
            
            var fineToPay = await _unitOfWork.FineRepository.GetFineByIdAsync(id);

            if (fineToPay == null)
            {
                return OperationResult.Failure(OperationStatus.NotFound, "Fine not found");
            }

            if (fineToPay.PaidAt != null || fineToPay.Status.Equals("Waived", StringComparison.OrdinalIgnoreCase))
            {
                return OperationResult.Failure(OperationStatus.Conflict, "Fine was already completed (Paid/Waived");
            }
             
            fineToPay.Status = "Paid";
            fineToPay.PaidAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return OperationResult.Success("Fine paid successfully");
        }
        public async Task<PagedList<FineDtoForDisplay>> GetFinesAsync(FineParameters parameters)
        {
            var pagedList = await _unitOfWork.FineRepository.GetFinesAsync(parameters);

            var fines = _mapper.Map<List<FineDtoForDisplay>>(pagedList.Items);

            return pagedList.MapTo(fines);
        }


    }
}
