using AnnapurnaEnterprises.Api.DTOs;

namespace AnnapurnaEnterprises.Api.Services.Interfaces
{
    public interface IAdminAuthService
    {
        Task<AdminLoginResponseDto?> LoginAsync(AdminLoginRequestDto request);
    }
}