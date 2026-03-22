using AnnapurnaEnterprises.Api.Data;
using AnnapurnaEnterprises.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace AnnapurnaEnterprises.Api.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db, IConfiguration config)
        {
            // ✅ Apply migrations automatically
            await db.Database.MigrateAsync();

            // ✅ Read from Environment Variables
            var username = config["AdminSettings:Username"];
            var password = config["AdminSettings:Password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("❌ Admin credentials not found in environment variables.");

            // ✅ Check if admin exists
            var exists = await db.AdminUsers.AnyAsync(x => x.Username == username);

            if (!exists)
            {
                db.AdminUsers.Add(new AdminUser
                {
                    Username = username,
                    PasswordHash = HashPassword(password)
                });

                await db.SaveChangesAsync();
            }
        }

        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}