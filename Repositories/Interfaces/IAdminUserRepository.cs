using AnnapurnaEnterprises.Api.Models;

namespace AnnapurnaEnterprises.Api.Repositories.Interfaces;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByUsernameAsync(string username);
}