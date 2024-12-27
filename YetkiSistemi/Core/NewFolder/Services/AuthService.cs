using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YetkiSistemi.Core.Entities;
using YetkiSistemi.Core.Interfaces;

namespace YetkiSistemi.Core.NewFolder.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            // Kullanıcı doğrulama
            var user = (await _unitOfWork.Users.FindAsync(u => u.Username == username && u.Password == password)).FirstOrDefault();
            return user;
        }

        public string GenerateJwtToken(User user)
        {
            // Kullanıcı bilgileriyle Claims oluştur
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("IsAdmin", user.IsAdmin.ToString()),
            new Claim("PermissionId", user.PermissionId.ToString() ?? string.Empty)
        };

            // JWT'nin imza anahtarı
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token'in ayarları
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
