using Application.Persistence.Services;

namespace Infrastructure.Persistence.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventAppDbContext _context;
        
        public UnitOfWork(EventAppDbContext context)
        {
            _context = context;
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.BeginTransactionAsync(cancellationToken);

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.CommitTransactionAsync(cancellationToken); 

        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.RollbackTransactionAsync(cancellationToken);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}