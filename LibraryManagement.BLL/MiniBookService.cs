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
    public class MiniBookService : IMiniBookService
    {
        private readonly IMiniBookRepository _miniBookRepository;

        public MiniBookService(IMiniBookRepository miniBookRepository)
        {
            _miniBookRepository = miniBookRepository;
        }
    }
}
