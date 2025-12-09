using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace LibraryManagement.BLL
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;

        public BorrowingService(IBorrowingRepository borrowingRepository)
        {
            _borrowingRepository = borrowingRepository;
        }
    }
}
