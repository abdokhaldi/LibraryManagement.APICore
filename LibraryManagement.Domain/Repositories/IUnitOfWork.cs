
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Repositories;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IBookCopyRepository BookCopyRepository { get; }
        IMemberRepository MemberRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IBorrowingRepository BorrowingRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPersonRepository PersonRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
