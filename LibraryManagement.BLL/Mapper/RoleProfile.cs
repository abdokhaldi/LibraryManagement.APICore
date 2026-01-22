
using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.RoleDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleForCreationDTO, Role>();
            CreateMap<Role, RoleForDisplayDTO>();
        }
    }
}
