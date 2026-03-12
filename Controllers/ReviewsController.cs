using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnnapurnaEnterprises.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IGenericRepository<Review> _repo;

    public ReviewsController(IGenericRepository<Review> repo)
    {
        _repo = repo;
    }

    // Public: list reviews
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list.Where(x => x.IsApproved).OrderByDescending(x => x.CreatedAt));
    }

    // Public: add review (auto post)
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var review = new Review
        {
            Name = dto.Name,
            Comment = dto.Comment,
            Rating = dto.Rating,
            IsApproved = true
        };

        await _repo.AddAsync(review);
        await _repo.SaveChangesAsync();

        return Ok(review);
    }

    // Admin delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _repo.GetByIdAsync(id);
        if (review == null) return NotFound();

        _repo.Remove(review);
        await _repo.SaveChangesAsync();

        return Ok(new { message = "Deleted successfully" });
    }
}