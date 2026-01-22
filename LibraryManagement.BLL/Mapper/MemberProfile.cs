using AutoMapper;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.Domain.Entities;


namespace LibraryManagement.BLL.Mapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberForCreationDTO, Member>();
            CreateMap<Member, MemberForDisplayDTO>()
                .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src =>
                   src.Person.FirstName + " " + src.Person.LastName
                    ));

        }
    }
}
