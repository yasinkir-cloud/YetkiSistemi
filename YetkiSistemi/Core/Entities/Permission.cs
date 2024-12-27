
namespace YetkiSistemi.Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
        public ICollection<PermissionDetail>? PermissionDetails { get; set; }
    }
}
