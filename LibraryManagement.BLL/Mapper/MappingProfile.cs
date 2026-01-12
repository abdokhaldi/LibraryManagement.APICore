using AutoMapper;
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.PersonDTOs;
using LibraryManagement.DTO.MemberDTOs;
using LibraryManagement.DTO.BorrowingDTOs;
using LibraryManagement.DTO.RoleDTOs;
using LibraryManagement.DTO.UserDTOs;
using LibraryManagement.DTO.CategoryDTOs;

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
                    dst => dst.CategoryName,
                    opt => opt.MapFrom(src => src.Category!.CategoryName)
                    );

            // Auto mapping for Borrowing
            CreateMap<BorrowingForCreationDTO,Borrowing>();
         var mappingBorrowingForDisplay =  CreateMap<Borrowing, BorrowingForDisplayDTO>()
                .ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src=>src.Member.Person.FirstName +" "+ src.Member.Person.LastName )
                );
            mappingBorrowingForDisplay.ForMember(
                dst => dst.Title,
                opt => opt.MapFrom(src => src.Book.Title)
                );


            CreateMap<BorrowingForExtendDTO, Borrowing>();

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
           mappingPersonForUpdate.ForMember(dst => dst.IsActive,
                 opt => {
                     opt.PreCondition(
                         s => s.IsActive.HasValue);
                     opt.MapFrom(s => s.IsActive!.Value);
                     opt.UseDestinationValue();
                 });

            // Roles
            CreateMap<RoleForCreationDTO,Role>();
            CreateMap<Role, RoleForDisplayDTO>();

            // Users
            CreateMap<UserForCreationDTO,User>();
           
            var mappingUserForDisplay = CreateMap<User, UserForDisplayDTO>();
           
            mappingUserForDisplay.ForMember(dst => dst.FullName,
                opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName)
                );
            mappingUserForDisplay.ForMember(dst => dst.RoleName,
                                  opt => opt.MapFrom(src =>src.Role.RoleName)
                );
            
            
          var mappingUserForUpdate = CreateMap<UserForUpdateDTO,User>();
            mappingUserForUpdate.ForAllMembers(
                opts => opts.Condition(
                    (src ,dst,srcMember) 
                   => {
                        return srcMember != null;
                    }
                    )
                );
            mappingUserForUpdate.ForMember(
                dst=>dst.IsActive,
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
                    opt.PreCondition(s=>s.IsBlocked.HasValue);
                    opt.MapFrom(s=>s.IsBlocked!.Value);
                    opt.UseDestinationValue();
                }
                );
            mappingUserForUpdate.ForMember(
                dst =>dst.RoleID ,
                opt =>
                {
                    opt.PreCondition(s=>s.RoleID.HasValue);
                    opt.MapFrom(s=>s.RoleID!.Value);
                } 
                );
            mappingUserForUpdate.ForMember(
                dst=>dst.PersonID,
                opt =>
                {
                    opt.PreCondition(s=>s.PersonID.HasValue);
                    opt.MapFrom(s=>s.PersonID!.Value);
                    opt.UseDestinationValue();
                }

                );

             CreateMap<CategoryForCreationDTO, Category>();
             CreateMap<CategoryForUpdateDTO, Category>()
                .ForAllMembers(opts =>
                opts.Condition(
                    (src, opt, srcMember) =>
                    {
                      return  srcMember != null;
                    })
                );
            CreateMap<Category, CategoryForDisplayDTO>();  

        }

    }
}
