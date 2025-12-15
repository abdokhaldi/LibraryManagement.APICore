using AutoMapper;
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Entities;
using LibraryManagement.DTO.PersonDTOs;

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
            // Auto mapping for Member
            // Auto mapping for Person
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
