using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.OperationResults;
using LibraryManagement.DTO.RoleDTOs;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.DTO.Common;
namespace LibraryManagement.BLL
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OperationResult<int>> CreateRoleAsync(RoleForCreationDTO roleDTO)
        {
            var rolesQuery = await _unitOfWork.RoleRepository.GetQueryableRolesAsync();
            bool exists = await rolesQuery.AnyAsync(r => r.RoleName.ToLower() == roleDTO.RoleName.ToLower());
            if (exists)
            {
                return OperationResult<int>.Failure(OperationStatus.Conflict,"role is already exists .");
            }
            var roleEntity = _mapper.Map<Role>(roleDTO);
            await _unitOfWork.RoleRepository.CreateRole(roleEntity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<int>.Success(roleEntity.RoleID);
        }
        public async Task<OperationResult<RoleForDisplayDTO>> GetRoleAsync(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetRoleForReadOnlyAsync(id);
            if (role == null)
            {
                return OperationResult<RoleForDisplayDTO>.Failure(OperationStatus.NotFound, $"Role with ID:{id} was not found.");

            }
            var roleDTO = _mapper.Map<RoleForDisplayDTO>(role);
            return OperationResult<RoleForDisplayDTO>.Success(roleDTO);
        }

    }
}
