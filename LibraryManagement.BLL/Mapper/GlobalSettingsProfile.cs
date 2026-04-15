using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.GlobalSettings;


namespace LibraryManagement.BLL.Mapper
{
    public class GlobalSettingsProfile : Profile
    {
        public GlobalSettingsProfile()
        {
            CreateMap<GlobalSettings, GlobalSettingsForReadOnlyDTO>();

          var mappingForUpdate = CreateMap<GlobalSettingsForUpdateDTO, GlobalSettings>();

            mappingForUpdate.ForAllMembers(
               src => src.Condition(
                   (src, dest, srcMember) =>
                   {
                       if (srcMember is null)
                           return false;
                       if (srcMember is string str)
                           return !string.IsNullOrEmpty(str);
                       return true;
                   })
                );
            
            mappingForUpdate.ForMember( dest => dest.DefaultFinePerDay,
                    opt =>
                    {
                        opt.PreCondition(
                            s => s.DefaultFinePerDay.HasValue);
                        opt.MapFrom(s => s.DefaultFinePerDay);
                        opt.UseDestinationValue();
                    }
                );

            mappingForUpdate.ForMember(dest => dest.MaxFineLimit,
                opt =>
                {
                    opt.PreCondition(s => s.MaxFineLimit.HasValue);
                    opt.MapFrom(s => s.MaxFineLimit);
                    opt.UseDestinationValue();
                });

            mappingForUpdate.ForMember(

                dest => dest.MaxBooksPerMember,

                opt =>
                {
                    opt.PreCondition(s => s.MaxBooksPerMember.HasValue);
                    opt.MapFrom(s => s.MaxBooksPerMember);
                    opt.UseDestinationValue();
                }
                  );

            mappingForUpdate.ForMember(
                dest => dest.DefaultBorrowingDays,
               opt => {
                   opt.PreCondition(s => s.DefaultBorrowingDays.HasValue);
                   opt.MapFrom(s => s.DefaultBorrowingDays);
                   opt.UseDestinationValue();
                }
            );

            mappingForUpdate.ForMember(
                dest => dest.IsLibraryOpen,
                opt => {
                    opt.PreCondition(s => s.IsLibraryOpen.HasValue);
                    opt.MapFrom(s => s.IsLibraryOpen!.Value);
                    opt.UseDestinationValue();
                }
                );

            
           }
        }
    }

