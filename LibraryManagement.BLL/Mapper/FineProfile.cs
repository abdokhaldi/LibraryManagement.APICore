using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.DTO.FineDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Mapper
{
    public class FineProfile : Profile
    {
        public FineProfile()
        {
            CreateMap<Fine, FineDtoForDisplay>();
            CreateMap<FineDtoForDisplay, Fine>();

        }
    }
}
