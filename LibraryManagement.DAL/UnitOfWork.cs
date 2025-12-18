
using LibraryManagement.DAL;
using LibraryManagement.DAL.Context;
using LibraryManagement.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork
{
  public IBookRepository BookRepository { get; }
  public IMemberRepository MemberRepository { get; }
  public IActivityRepository ActivityRepository { get; }
  public IBorrowingRepository BorrowingRepository { get; }
  public ICategoryRepository CategoryRepository { get; }
  public IPersonRepository PersonRepository { get; }
  public IRoleRepository RoleRepository { get; }
  public IUserRepository UserRepository { get; }
  
    private readonly LibraryDbContext _context;
   

    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;

        BookRepository =    new BookRepository(_context);
        MemberRepository =    new MemberRepository(_context);
        ActivityRepository   = new ActivityRepository(_context);
        BorrowingRepository = new BorrowingRepository(_context);
        CategoryRepository   = new CategoryRepository(_context);
        PersonRepository     = new PersonRepository(_context);
        RoleRepository       = new RoleRepository(_context);
        UserRepository = new UserRepository(_context);
        }

    
    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}