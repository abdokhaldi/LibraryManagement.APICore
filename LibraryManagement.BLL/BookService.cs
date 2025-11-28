using DAL_LibraryManagement;
using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BLL_LibraryManagement
{
    public class BookService
    {
        public enum Mode { AddNew = 1, Update = 2 }
        public int BookID { get; private set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string YearPublished { get; set; } 
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageBytesToSave { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; private set; }
        public CategoryService CategoryInfo;
        public Mode _Mode = Mode.AddNew;

        public BookService(int bookID, string title, string author, string publisher, string yearPublisher, int quantity,string image, int categoryID, bool isActive)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Publisher = publisher;
            YearPublished = yearPublisher;
            Quantity = quantity;
            ImagePath = image;
            CategoryID = categoryID;
            IsActive = isActive;
            CategoryInfo = CategoryService.GetCategoryByID(CategoryID);
            Category = CategoryInfo.CategoryName;
            _Mode = Mode.Update;
            
        }
        public BookService()
        {
            
            Title = "";
            Author = "";
            Publisher = "";
            YearPublished = "";
            Quantity = 0;
            ImagePath = "";
            CategoryID = 0;
            IsActive = true;

            _Mode = Mode.AddNew;
            
        }


        public void ImageToSave ()
        {
            // ... منطق التحقق من البيانات الأخرى ...

            // ** 1. استدعاء معالج الصور هنا (القلب النابض للتحسين) **
            if (this.ImageBytesToSave != null && this.ImageBytesToSave.Length > 0)
            {
                // 1. حفظ المسار القديم
                string oldFileName = this.ImagePath;

                // 2. معالجة وحفظ الصورة الجديدة (وتحديث this.ImagePath بالاسم الجديد)
              // نستدعي الدالة التي تنفذ: التصغير -> الحفظ على القرص -> إرجاع اسم الملف
                string savedFileName = ImageProcessorService.SaveImageToFileSystem(this.ImageBytesToSave);

                // نحدث الخاصية التي ستُخزن في قاعدة البيانات
                this.ImagePath = savedFileName;
                
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    // نستدعي دالة جديدة في ImageProcessorService للقيام بالحذف الآمن
                    ImageProcessorService.DeleteImageFile(oldFileName);
                }
            }
            else
            {
                // إذا لم يتم إرسال صورة جديدة، نحافظ على المسار الحالي في وضع التعديل
                // أو نتركه فارغاً إذا كنا في وضع الإضافة
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    this.ImagePath = string.Empty;
                }
            }
 }



        private BookDTO _FillDTOToTransferData()
        {
            return new BookDTO
            {
                BookID = BookID,
                Title = Title,
                Author = Author,
                Publisher = Publisher,
                YearPublished = YearPublished,
                Quantity = Quantity,
                Image = ImagePath,
                CategoryID = CategoryID,
                
                IsActive = IsActive ,
            };
        }
        public static List<BookService> GetAllBooks()
        {
            var booksList = BookRepository.GetAllBooks();
 
            if (booksList == null) return null;

            var books = booksList.Select( b  =>
                new BookService(b.BookID, b.Title, b.Author,
                    b.Publisher, b.YearPublished, b.Quantity, b.Image, b.CategoryID,b.IsActive)
                ).ToList();

            return books;
        }
        private bool _AddNewBook()
        {
            var data = _FillDTOToTransferData();

             this.BookID = BookRepository.AddNewBook(data);
            
            if (BookID > 0)
            {
                ActivityRepository.AddActivity("Add", $"Add book: {Title}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Book", BookID);
                return true;
            }
            return false;
        }

        private bool _UpdateBookByID()
        {
            var data = _FillDTOToTransferData();

            int rowsAffected = BookRepository.UpdateBookByID(data);
            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Update", $"Update book: {Title}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "Book", BookID);
                return true;
            }
            return false;
        }

        public OperationResultBLL Save()
        {
            ImageToSave();
            switch (_Mode)
            {
                case Mode.AddNew :
                    if (_AddNewBook())
                    {
                        _Mode = Mode.Update;
                        return OperationResultBLL.Ok("book has been added successfully .");
                    }
                    return OperationResultBLL.Fail("book was not added!");


                case Mode.Update:
                    if (_UpdateBookByID())
                    {

                        return OperationResultBLL.Ok("book has been updated successfully .");
                    }
                    return OperationResultBLL.Fail("book was not updated!");
            }
            return OperationResultBLL.Fail("Oops... unexpected error occurs !");
        }
        public static BookService FindBookByID(int id)
        {
            
          BookDTO bookDTO = BookRepository.FindBookByID(id);

            if (bookDTO == null) return null;
            
               return new BookService(bookDTO.BookID, bookDTO.Title, bookDTO.Author, bookDTO.Publisher, bookDTO.YearPublished, bookDTO.Quantity, bookDTO.Image, bookDTO.CategoryID,bookDTO.IsActive);
        }

        public static bool IsBookExist(int id) 
        {
            var book = FindBookByID(id);
            return book != null;
        }
        

        public bool DeleteBookByID(int bookID)
        {
            return BookRepository.DeleteBookByID(bookID);
        
        }

        public static int GetQuantity(int id)
        {
            object quantity = BookRepository.GetBookQuantity(id);
            return Convert.ToInt32(quantity);
        }

        public static OperationResultBLL DeactivateBook(BookService book)
        {
            int rowsAffected = BookRepository.SetBookActiveStatus(book.BookID, false);

            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Deactivate", $"Deactivated book: {book.Title}", DateTime.Now,CurrentUser.GetCurrentUserInfo().Username, "Book", book.BookID);
                return OperationResultBLL.Ok("Book Deactivated .");
            }

            return OperationResultBLL.Fail("Book cannot be deactivated .");
        }
        public static OperationResultBLL ActivateBook(BookService book)
        {
            int rowsAffected = BookRepository.SetBookActiveStatus(book.BookID, true);
            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Reactivate", $"Reactivated book : {book.Title}", DateTime.Now, "", "Book", book.BookID);
                return OperationResultBLL.Ok("Book Reactivated .");
            }

            return OperationResultBLL.Ok("Book cannot be Reactivated .");
        }


    }
}
