using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.BookCopyDTO;
using LibraryManagement.Shared.Types;


namespace LibraryManagement.BLL.Mapper
{
    public class BookCopyProfile : Profile
    {

        public BookCopyProfile()
        {
            CreateMap<BookCopyForCreationDTO, BookCopy>();

            var mappingForUpdate = CreateMap<BookCopyForUpdateDTO, BookCopy>();
                mappingForUpdate.ForAllMembers(opts =>
                opts.Condition(
                    (src, dst, srcMember) =>
                    {
                        return srcMember != null;
                    }
                 )
                );
            

            mappingForUpdate.ForMember(dst =>
            dst.IsActive,
            opt => {
                opt.PreCondition(s =>
                     s.IsActive.HasValue);
                opt.MapFrom(s => s.IsActive!.Value);
                opt.UseDestinationValue();
                } );

            mappingForUpdate.ForMember(dst
                     => dst.Status,
                opt => {
                    opt.PreCondition(s => s.Status.HasValue);
                    opt.MapFrom(s => s.Status!.Value);
                    opt.UseDestinationValue();
                    });

                

            var mappingForDisplay = CreateMap<BookCopy, BookCopyForDisplayDTO>();

            mappingForDisplay.ForMember(
                 dst => dst.BookTitle,
                 opt => opt.MapFrom(s => s.Book!.Title)
                );

            mappingForDisplay.ForMember(
                 dst => dst.ISBN,
                 opt => opt.MapFrom(s => s.Book!.ISBN)
                );

            mappingForDisplay.ForMember(
                 dst => dst.DateAdded,
                 opt => opt.MapFrom(s => s.Book!.CreatedAt)
                );
            mappingForDisplay.ForMember(
                 dst => dst.CanBeBorrowed,
                 opt => opt.MapFrom(s => s.IsActive && s.Status==CopyStatus.Available)
                );

            mappingForDisplay.ForMember(
                dst => dst.Status,
                opt => opt.MapFrom(s =>
                s.Status == CopyStatus.Available ? "Available" :
                s.Status == CopyStatus.Borrowed ? "Borrowed" :
                s.Status == CopyStatus.Damaged ? "Damaged" :
                s.Status == CopyStatus.Lost ? "Lost" : "Reserved"
                )
            );

         

        }
    }
}
