using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using AnnapurnaEnterprises.Api.Seed;
using AnnapurnaEnterprises.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnnapurnaEnterprises.Api.Services
{
    public class AdminAuthService : IAdminAuthService
    {
        private readonly IAdminUserRepository _repo;
        private readonly IConfiguration _config;

        public AdminAuthService(IAdminUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<AdminLoginResponseDto?> LoginAsync(AdminLoginRequestDto request)
        {
            var admin = await _repo.GetByUsernameAsync(request.Username);
            if (admin == null) return null;

            var isValid = DbSeeder.VerifyPassword(request.Password, admin.PasswordHash);
            if (!isValid) return null;

            var expiryMinutes = 120;
            if (int.TryParse(_config["Jwt:ExpiryInMinutes"], out var minutes))
            {
                expiryMinutes = minutes;
            }

            var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);
            var token = GenerateJwtToken(admin.Username, expiresAt);

            return new AdminLoginResponseDto
            {
                Username = admin.Username,
                Token = token,
                ExpiresAtUtc = expiresAt
            };
        }

        private string GenerateJwtToken(string username, DateTime expiresAtUtc)
        {
            var key = _config["Jwt:Key"] ?? throw new Exception("Jwt:Key missing in appsettings.json");
            var issuer = _config["Jwt:Issuer"] ?? "AnnapurnaEnterprises";
            var audience = _config["Jwt:Audience"] ?? "AnnapurnaEnterprisesUsers";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAtUtc,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}