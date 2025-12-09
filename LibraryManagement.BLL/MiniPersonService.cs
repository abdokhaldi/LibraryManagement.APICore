using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public class MiniPersonService : IMiniPersonService
    {
        private readonly IMiniPersonRepository _miniPersonRepository;

        public MiniPersonService(IMiniPersonRepository miniPersonRepository)
        {
            _miniPersonRepository = miniPersonRepository;
        }
    }
}
