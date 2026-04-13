using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.BorrowingDTOs;


namespace LibraryManagement.BLL.Mapper
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<BorrowingForCreationDTO, Borrowing>();

            var mappingBorrowingForDisplay = CreateMap<Borrowing, BorrowingForDisplayDTO>()
                   .ForMember(dst => dst.FullName,
                   opt => opt.MapFrom(src => src.Member.Person.FirstName + " " + src.Member.Person.LastName)
                   );

            mappingBorrowingForDisplay.ForMember(
                dst => dst.Title,
                opt => opt.MapFrom(src => src.BookCopy.Book!.Title)
                );
            mappingBorrowingForDisplay.ForMember(dest => dest.Barcode,
                opt => opt.MapFrom(src => src.BookCopy.Barcode)
                );
           mappingBorrowingForDisplay.ForMember(
               dest => dest.Status,
               opt => opt.MapFrom(src => ( src.DueDate < DateTime.UtcNow ) && (src.ReturnDate==null)? "Overdue" : src.Status)
               );
            CreateMap<BorrowingForExtendDTO, Borrowing>();
        }
    }
}
