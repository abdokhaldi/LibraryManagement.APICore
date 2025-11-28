using DAL_LibraryManagement;
using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace BLL_LibraryManagement
{
    public class BorrowingService
    {
        public enum Mode {AddNew=1,Update=2,EditDueDateOnly=3 }
        public int BorrowingID { get; private set; }
        public int BookID { get; set; }
        public bool IsCanceled { get; set; }
        public int PersonID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate{ get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }

        public readonly bool IsActive;

        public Mode _Mode = Mode.AddNew;

        public BorrowingService(int borrowingID, int bookID, int memberID, DateTime borrowingDate, DateTime dueDate, DateTime? returnDate, string status,bool isCanceled)
        {
            BorrowingID = borrowingID;
            BookID = bookID;
            PersonID = memberID;
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ReturnDate = returnDate;
            Status = status;
            IsCanceled = isCanceled;
            IsActive = _IsActiveBorrowing(BookID,PersonID);
            _Mode = Mode.Update;
        }
        public BorrowingService()
        {
           
            BookID = -1;
            PersonID = -1;
            BorrowingDate = DateTime.Now;
            DueDate = DateTime.Now;
            ReturnDate = null;
            Status = "Borrowed";
            IsActive = true;
            _Mode = Mode.AddNew;
        }

       

        private BorrowingDTO FillCurrentDataToTransfer()
        {
            return new BorrowingDTO
            {
                BookID = BookID,
                PersonID = PersonID,
                BorrowingDate = BorrowingDate,
                DueDate = DueDate,
                ReturnDate = ReturnDate,
                Status = Status,
                IsCanceled = IsCanceled,
            };
           }


        private bool _AddNewBorrowing()
        {
            var borrowingData = FillCurrentDataToTransfer();
            
            this.BorrowingID = BorrowingRepository.AddBorrowingAndUpdateBook(borrowingData);
            if (BorrowingID <= 0) return false;
            if (!MemberService.IsPersonAsMember(borrowingData.PersonID))
            {
                MemberRepository.AddNewMember(borrowingData.PersonID,DateTime.Now,true);
            }
            
                ActivityRepository.AddActivity("Add", $"Add borrowing: {BorrowingID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Borrowing", BorrowingID);
                
            return this.BorrowingID > 0;
        }

        private bool _UpdateBorrowing()
        {
            var borrowingData = FillCurrentDataToTransfer();
            int rowsAffected = BorrowingRepository.UpdateBorrowing(borrowingData);
           if( rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Update", $"Change borrowing due date: {BorrowingID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Borrowing", BorrowingID);
                return true;
            }
            return false;
        }
        public static OperationResultBLL EditDueDate(int borrowingID,DateTime dueDate)
        {
            int rowsAffected = BorrowingRepository.EditDueDate(borrowingID,dueDate);
            if( rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Edit", $"Change borrowing due date: {borrowingID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Borrowing", borrowingID);
                return OperationResultBLL.Ok("Due date was changed successfully!");
            }
            return OperationResultBLL.Fail("fail: Due date was not changed!");
        }

        private bool _IsActiveBorrowing(int bookID,int memberID)
        {
            var result = BorrowingRepository.IsActiveBorrowing(bookID,memberID);
            return result != null;
        }
        private bool IsAvailableForBorrowing()
        {
           return  (BookService.GetQuantity(BookID) >0);
        }


        public OperationResultBLL Save()
        {
            switch (_Mode)
            {
                case Mode.AddNew:

                    if (!IsAvailableForBorrowing())
                        return OperationResultBLL.Fail("The book is not available for borrow!");
                   
                    if (_AddNewBorrowing())
                    {
                        _Mode = Mode.Update;
                        return OperationResultBLL.Ok("The book has been successfully borrowed .");
                    }
                    else
                    {
                        return OperationResultBLL.Fail("The borrowing cannot be added!");
                    }

                       
                case Mode.Update:

                    if (!IsActive)
                        return OperationResultBLL.Fail("This borrowing cannot be updated,because it is inactive.");
                   
                    if (_UpdateBorrowing())
                        return OperationResultBLL.Ok("The borrowing has been updated successfully .");
                 
                    break;
            }
            return OperationResultBLL.Fail();
        }
        
        public static List<BorrowingService> GetAllBorrowings()
        {
            var borrowingsList = BorrowingRepository.GetAllBorrowings();

            if (borrowingsList == null) return null;

            var borrowings = borrowingsList.Select(
                b => new BorrowingService(b.BorrowingID, b.BookID, b.PersonID, b.BorrowingDate, b.DueDate, b.ReturnDate, b.Status,b.IsCanceled)
              ).ToList();

            return borrowings;
        }

        public static BorrowingService FindBorrowingByID(int id)
        {
            var dtoInfo = BorrowingRepository.FindBorrowingByID(id);
           if(dtoInfo == null) return null;
            return new BorrowingService(dtoInfo.BorrowingID,dtoInfo.BookID,dtoInfo.PersonID,dtoInfo.BorrowingDate,dtoInfo.DueDate,dtoInfo.ReturnDate,dtoInfo.Status,dtoInfo.IsCanceled);
        }

        public static OperationResultBLL CancelBorrowing(int borrowingID,int bookID)
        {
            
               
            bool IsCanceled = BorrowingRepository.CancelBorrowing(borrowingID,bookID);
            if (IsCanceled)
            {
                ActivityRepository.AddActivity("Cancel", $"Cancel borrowing: {borrowingID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Borrowing", borrowingID);
                return OperationResultBLL.Ok("Borrowing canceled.");
            }
                return OperationResultBLL.Fail("Failed:Borrowing was not canceled!");
        }


        public OperationResultBLL ReturnBookAndRestock()
        {
            if (ReturnDate != null)
                return OperationResultBLL.Fail($"Book [{BookID}] has already been returned.");
            

            int newQuantity = BookService.GetQuantity(BookID) + 1;

            if (BorrowingRepository.ReturnBookAndRestock(BorrowingID, BookID, DateTime.Now, newQuantity))
            {
                ActivityRepository.AddActivity("Return", $"Return book and complete borrowing: {BorrowingID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Borrowing", BorrowingID);

                return OperationResultBLL.Ok($"The book [{BookID}] was successfully returned.");
            }
            return OperationResultBLL.Fail($"The return operation for book [{BookID}] failed.");
        }    
        
    }
}
