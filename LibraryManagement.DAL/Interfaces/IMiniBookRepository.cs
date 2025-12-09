using LibraryManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IMiniBookRepository
    {
        Task<IQueryable<SmallBookEntity>> GetQueryableSmallBooksAsync();
       
    }
}
