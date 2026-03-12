using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnnapurnaEnterprises.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminAuthController : ControllerBase
{
    private readonly IAdminAuthService _auth;

    public AdminAuthController(IAdminAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AdminLoginRequestDto request)
    {
        var result = await _auth.LoginAsync(request);
        if (result == null) return Unauthorized(new { message = "Invalid username or password" });

        return Ok(result);
    }
}