using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.BLL
{
    public class BorrowingInfoService
    {
        public int BorrowingID { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        private BorrowingInfoService(BorrowingInfoDTO borrowingDTO)
        {
            BorrowingID = borrowingDTO.BorrowingID;
            BookID = borrowingDTO.BookID;
            Title = borrowingDTO.Title;
            FullName = borrowingDTO.FullName;
            BorrowingDate = borrowingDTO.BorrowingDate;
            DueDate = borrowingDTO.DueDate;
            ReturnDate = borrowingDTO.ReturnDate;
            Status = borrowingDTO.Status;
        }


        public static List<BorrowingInfoService> GetAllBorrowingsInfo()
        {
            var list = BorrowingInfoRepository.GetAllBorrowings();
            if (list == null) return null;
            var borrowings = list.Select( b => new BorrowingInfoService(b)).ToList();
            return borrowings;
        }
        

    }
}
