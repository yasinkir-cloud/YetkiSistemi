using Microsoft.EntityFrameworkCore;
using YetkiSistemi.Core.Entities;

namespace YetkiSistemi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionDetail> PermissionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User-Permission Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Permission)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.PermissionId);

            // PermissionDetail-Permission Relationship
            modelBuilder.Entity<PermissionDetail>()
                .HasOne(pd => pd.Permission)
                .WithMany(p => p.PermissionDetails)
                .HasForeignKey(pd => pd.PermissionId);

            // PermissionDetail-Page Relationship
            modelBuilder.Entity<PermissionDetail>()
                .HasOne(pd => pd.Page)
                .WithMany()
                .HasForeignKey(pd => pd.PageId);
        }
    }
}
