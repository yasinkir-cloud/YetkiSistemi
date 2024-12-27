using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

namespace YetkiSistemi.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<User> Users { get; }
        public IRepository<Page> Pages { get; }
        public IRepository<Permission> Permissions { get; }
        public IRepository<PermissionDetail> PermissionDetails { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Pages = new GenericRepository<Page>(_context);
            Permissions = new GenericRepository<Permission>(_context);
            PermissionDetails = new GenericRepository<PermissionDetail>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
