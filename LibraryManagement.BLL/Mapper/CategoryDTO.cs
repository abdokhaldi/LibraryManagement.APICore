using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.CategoryDTOs;

namespace LibraryManagement.BLL.Mapper
{
    public class CategoryDTO : Profile
    {
        public CategoryDTO()
        {
            CreateMap<CategoryForCreationDTO, Category>();
            CreateMap<CategoryForUpdateDTO, Category>()
               .ForAllMembers(opts =>
               opts.Condition(
                   (src, opt, srcMember) =>
                   {
                       return srcMember != null;
                   })
               );
            CreateMap<Category, CategoryForDisplayDTO>();
        }
    }
}
