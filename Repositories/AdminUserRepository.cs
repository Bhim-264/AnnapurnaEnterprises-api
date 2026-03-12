using AnnapurnaEnterprises.Api.Data;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnnapurnaEnterprises.Api.Repositories;

public class AdminUserRepository : IAdminUserRepository
{
    private readonly AppDbContext _db;

    public AdminUserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<AdminUser?> GetByUsernameAsync(string username)
    {
        return await _db.AdminUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username);
    }
}