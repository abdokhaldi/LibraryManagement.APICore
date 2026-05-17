using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.CategoryDTOs;

namespace LibraryManagement.BLL.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
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
