using YetkiSistemi.Core.Entities;

namespace YetkiSistemi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Authenticate(string username, string password);
        string GenerateJwtToken(User user);
    }
}
