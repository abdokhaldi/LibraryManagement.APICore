using AutoMapper;
using LibraryManagement.DTO.BookDTOs;
using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DAL.Entities;

namespace LibraryManagement.BLL.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<BookForCreationDTO, Book>();
            CreateMap<BookForUpdateDTO, Book>();
          
            CreateMap<Book, BookForDisplayDTO>()
             .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.CategoryName)
                    );
            
        }
    }
}
