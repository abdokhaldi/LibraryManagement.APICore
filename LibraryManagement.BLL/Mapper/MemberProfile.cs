using AutoMapper;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.FineDTO;


namespace LibraryManagement.BLL.Mapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            

            CreateMap<MemberForCreationDTO, Member>();
          var mappingForDisplay = CreateMap<Member, MemberForDisplayDTO>()
                .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src =>
                   src.Person.FirstName + " " + src.Person.LastName
                    ));

            
        }
    }
}
