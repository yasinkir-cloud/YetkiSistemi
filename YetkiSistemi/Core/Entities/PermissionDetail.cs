
namespace YetkiSistemi.Core.Entities
{
    public class PermissionDetail
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }

        public int PageId { get; set; }
        public Page? Page { get; set; }

        public bool CanList { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
