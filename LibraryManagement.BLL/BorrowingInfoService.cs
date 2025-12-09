using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.BLL
{
    public class BorrowingInfoService : IBorrowingInfoService
    {
        private readonly IBorrowingInfoRepository _borrowingInfoRepository;

        public BorrowingInfoService(IBorrowingInfoRepository borrowingInfoRepository)
        {
            _borrowingInfoRepository = borrowingInfoRepository;
        }
    }
}
