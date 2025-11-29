using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public class MinilBookService
    {
        public int BookID { get; set; }
        public string Title { get; set; }

        public MinilBookService(int bookID,string title)
        {
            BookID = bookID;
            Title = title;
        }

        public static List<MinilBookService> GetBookAutoSearch(string searchTerm)
        {
            List<SmallBookDTO> bookDTOs = SmallBookRepository.GetSmallBookAutoSearch(searchTerm);
           List<MinilBookService> booksFounded = null;
           if (bookDTOs == null) return null;
            booksFounded = bookDTOs.Select(p => new MinilBookService(p.BookID,p.Title)).ToList();
            return booksFounded;
        }
    }
}
