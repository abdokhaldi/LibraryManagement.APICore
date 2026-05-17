
using LibraryManagement.DAL.Context;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;


namespace LibraryManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
       
        public IBookRepository BookRepository { get; }
        public IBookCopyRepository BookCopyRepository { get; }

        public IMemberRepository MemberRepository { get; }
        public IActivityRepository ActivityRepository { get; }
        public IBorrowingRepository BorrowingRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IFineRepository FineRepository { get; }
        public IGlobalSettingsRepository GlobalSettingsRepository { get; }
        public ITenantRepository TenantRepository { get; }

        private readonly LibraryDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
           
            BookRepository = new BookRepository(_context);
            BookCopyRepository = new BookCopyRepository(_context);
            MemberRepository = new MemberRepository(_context);
            ActivityRepository = new ActivityRepository(_context);
            BorrowingRepository = new BorrowingRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            PersonRepository = new PersonRepository(_context);
            RoleRepository = new RoleRepository(_context);
            UserRepository = new UserRepository(_context);
            FineRepository = new FineRepository(_context);
            GlobalSettingsRepository = new GlobalSettingsRepository(_context);
            TenantRepository = new TenantRepository(_context);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction =  await _context.Database.BeginTransactionAsync();
        }
        
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_currentTransaction != null) await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context.Dispose();
        }
    }
}