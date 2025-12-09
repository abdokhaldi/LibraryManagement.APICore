using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IMemberRepository MemberRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IBorrowingInfoRepository BorrowingInfoRepository { get; }
        IBorrowingRepository BorrowingRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPersonRepository PersonRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
