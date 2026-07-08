using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.UserDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public  class UserProfile : Profile
    {
       public UserProfile()
        {
            CreateMap<UserForCreationDTO, User>()
                .ForMember(dest => dest.Person, opt => opt.Ignore());
                
            CreateMap<UserForAdminCreationDTO, User>();

            var mappingUserForDisplay = CreateMap<User, UserForDisplayDTO>();

            var mappingUserForUpdate = CreateMap<UserForUpdateDTO, User>();
            mappingUserForUpdate.ForAllMembers(
                opts => opts.Condition(
                    (src, dst, srcMember)
                   => {
                       return srcMember != null;
                   }
                    )
                );
            
            
            mappingUserForUpdate.ForMember(
                dst => dst.RoleID,
                opt =>
                {
                    opt.PreCondition(s => s.RoleID.HasValue);
                    opt.MapFrom(s => s.RoleID!.Value);
                }
                );
            mappingUserForUpdate.ForMember(
                dst => dst.PersonID,
                opt =>
                {
                    opt.PreCondition(s => s.PersonID.HasValue);
                    opt.MapFrom(s => s.PersonID!.Value);
                    opt.UseDestinationValue();
                }

                );
            mappingUserForUpdate.ForMember(
                dest => dest.Person,
                opt => opt.Ignore()
                );
        }
    }
}
