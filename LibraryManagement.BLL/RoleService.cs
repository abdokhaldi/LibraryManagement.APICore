using AutoMapper;
using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.RoleDTOs;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> CreateRoleAsync(RoleForCreationDTO roleDTO)
        {
            var rolesQuery = await _unitOfWork.RoleRepository.GetQueryableRolesAsync();
            bool exists = await rolesQuery.AnyAsync(r => r.RoleName.ToLower() == roleDTO.RoleName.ToLower());
            if (exists)
            {
                return -1;
            }
            var roleEntity = _mapper.Map<Role>(roleDTO);
            await _unitOfWork.RoleRepository.CreateRole(roleEntity);
            await _unitOfWork.SaveChangesAsync();

            return roleEntity.RoleID;
        }
        public async Task<RoleForDisplayDTO?> GetRoleAsync(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetRoleForReadOnlyAsync(id);
            if (role == null)
            {
                return null;
            }
            var roleDTO = _mapper.Map<RoleForDisplayDTO>(role);
            return roleDTO;
        }

    }
}
