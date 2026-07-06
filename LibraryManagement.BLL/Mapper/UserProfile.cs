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
                //.ForMember(dest => dest.PersonID, opt => opt.Ignore());
                
            CreateMap<UserForAdminCreationDTO, User>();

            var mappingUserForDisplay = CreateMap<User, UserForDisplayDTO>();

            mappingUserForDisplay.ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName)
                );
            mappingUserForDisplay.ForMember(dst => dst.RoleName,
                                  opt => opt.MapFrom(src => src.Role.RoleName)
                );


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
                dst => dst.IsActive,
                opt =>
                {
                    opt.PreCondition(s => s.IsActive.HasValue);
                    opt.MapFrom(s => s.IsActive!.Value);
                }
               );
            mappingUserForUpdate.ForMember(
                dst => dst.IsBlocked,
                opt =>
                {
                    opt.PreCondition(s => s.IsBlocked.HasValue);
                    opt.MapFrom(s => s.IsBlocked!.Value);
                    opt.UseDestinationValue();
                }
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
        }
    }
}
