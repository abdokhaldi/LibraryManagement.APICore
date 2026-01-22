using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.BookDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public class BookProfile : Profile
    {
      public  BookProfile()
        {
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
                    opt.MapFrom(s => s.CategoryID!.Value);
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
        }
    }
}
