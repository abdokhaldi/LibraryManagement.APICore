using AutoMapper;
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.DTO.BorrowingDTOs;

namespace LibraryManagement.BLL.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Auto mapping for Book
            CreateMap<BookForCreationDTO, Book>();

            var mapping = CreateMap<BookForUpdateDTO, Book>();
            mapping.ForMember(d => d.IsActive,
                  opt => {
                      opt.PreCondition(
                          s => s.IsActive.HasValue);
                      opt.MapFrom(s => s.IsActive!.Value);
                      opt.UseDestinationValue();
                  });
            mapping.ForMember(d => d.Quantity,
                 opt => {
                     opt.PreCondition(
                         s => s.Quantity.HasValue);
                     opt.MapFrom(s => s.Quantity!.Value);
                     opt.UseDestinationValue();
                 });
            mapping.ForMember(
                d => d.CategoryID,
                opt => {
                    opt.PreCondition(s =>
                s.CategoryID.HasValue);
                    opt.MapFrom(s=>s.CategoryID!.Value);
                });

            mapping.ForAllMembers(opts =>
                opts.Condition(
                    (src, dest, srcMember) =>
                {
                    return srcMember != null;
                })
            );

            CreateMap<Book, BookForDisplayDTO>()
             .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category!.CategoryName)
                    );

            // Auto mapping for Borrowing
            CreateMap<BorrowingForCreationDTO,Borrowing>();
            CreateMap<BorrowingForDisplayDTO, Borrowing>();
            CreateMap<BorrowingForUpdateDTO, Borrowing>();

            // Auto mapping for Member
            CreateMap<MemberForCreationDTO,Member>();
            CreateMap<Member, MemberForDisplayDTO>()
                .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src =>
                   src.Person.FirstName + " " + src.Person.LastName
                    ));


            // Auto mapping for Member
            CreateMap<PersonForCreationDTO, Person>();
          
            var personMappingForDisplay = CreateMap<Person, PersonForDisplayDTO>();
                personMappingForDisplay.ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src=>src.FirstName+" "+src.LastName)
                );

            personMappingForDisplay.ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender=='M'?"Male":"Female")
                );

            var mappingPersonForUpdate = CreateMap<PersonForUpdateDTO,Person>();
            mappingPersonForUpdate.ForAllMembers(
                opts=> opts.Condition(
                    (src, dst, srcMember) =>
                    {
                        return srcMember != null;
                    })
                );
            mappingPersonForUpdate.ForMember(d => d.IsActive,
                  opt => {
                      opt.PreCondition(
                          s => s.IsActive.HasValue);
                      opt.MapFrom(s => s.IsActive!.Value);
                      opt.UseDestinationValue();
                  });
        }
        
    }
}
