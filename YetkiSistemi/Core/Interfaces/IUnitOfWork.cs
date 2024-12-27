using YetkiSistemi.Core.Entities;

namespace YetkiSistemi.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Page> Pages { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<PermissionDetail> PermissionDetails { get; }
        Task SaveAsync();
    }
}
