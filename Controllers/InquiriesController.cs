using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnnapurnaEnterprises.Api.Controllers;

[ApiController]
[Route("api/inquiries")]
public class InquiriesController : ControllerBase
{
    private readonly IGenericRepository<Inquiry> _repo;

    public InquiriesController(IGenericRepository<Inquiry> repo)
    {
        _repo = repo;
    }

    // Public create inquiry
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] InquiryCreateDto dto)
    {
        var inquiry = new Inquiry
        {
            Name = dto.Name,
            Mobile = dto.Mobile,
            Message = dto.Message
        };

        await _repo.AddAsync(inquiry);
        await _repo.SaveChangesAsync();

        return Ok(new { message = "We will call you soon." });
    }

    // Admin list inquiries
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list.OrderByDescending(x => x.CreatedAt));
    }

    // Admin delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var inquiry = await _repo.GetByIdAsync(id);
        if (inquiry == null) return NotFound();

        _repo.Remove(inquiry);
        await _repo.SaveChangesAsync();

        return Ok(new { message = "Deleted successfully" });
    }
}