using AnnapurnaEnterprises.Api.Data;
using AnnapurnaEnterprises.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AnnapurnaEnterprises.Api.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            // ✅ Apply migrations automatically
            await db.Database.MigrateAsync();

            // ✅ Seed default admin
            const string username = "satya2026";
            const string password = "Satya@2025";

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