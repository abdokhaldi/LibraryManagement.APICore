using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.PersonDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonForCreationDTO, Person>();

            var personMappingForDisplay = CreateMap<Person, PersonForDisplayDTO>();
            personMappingForDisplay.ForMember(dst => dst.FullName,
            opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)
            );

            

            personMappingForDisplay.ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender == 'M' ? "Male" : "Female")
                );
            
            var mappingPersonForUpdate = CreateMap<PersonForUpdateDTO, Person>();
            mappingPersonForUpdate.ForAllMembers(
                opts => opts.Condition(
                    (src, dst, srcMember) =>
                    {
                        return srcMember != null;
                    })
                );
            
        }
    }
}
