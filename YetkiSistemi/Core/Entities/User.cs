
namespace YetkiSistemi.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
 
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
