using AutoMapper;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.DTO.TenantDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public class TenantProfile : Profile
    {
        public TenantProfile() {
            CreateMap<TenantForCreationDTO, Tenant>()
                    .ForAllMembers(opt => opt.Condition((src, dst, srcMember) =>
                    {
                        return srcMember != null;
                    }

                   )
                );

            var mapForUpdate = CreateMap<TenantForUpdateDTO, Tenant>();
                   
            mapForUpdate.ForAllMembers(opt => opt.Condition((src, dst, srcMember) =>
                    {
                        return srcMember != null;
                    }
                    )
                );
            mapForUpdate.ForMember(dst => dst.IsActive,
                   opt => {
                       opt.PreCondition(
                           s => s.IsActive.HasValue);
                       opt.MapFrom(s => s.IsActive!.Value);
                       opt.UseDestinationValue();
                   });

             CreateMap<Tenant, TenantForDisplayDTO>();
        }
    }
}
