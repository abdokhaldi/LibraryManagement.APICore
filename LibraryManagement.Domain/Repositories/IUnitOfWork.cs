
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
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
