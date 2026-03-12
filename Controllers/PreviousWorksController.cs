using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnnapurnaEnterprises.Api.Controllers;

[ApiController]
[Route("api/previous-works")]
public class PreviousWorksController : ControllerBase
{
    private readonly IPreviousWorkRepository _repo;

    public PreviousWorksController(IPreviousWorkRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repo.GetAllWithImagesAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdWithImagesAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] PreviousWorkCreateUpdateDto dto)
    {
        if (dto.ImageUrls.Count > 5)
            return BadRequest(new { message = "Maximum 5 images allowed." });

        var work = new PreviousWork
        {
            Title = dto.Title,
            Description = dto.Description,
            Images = dto.ImageUrls.Select(x => new PreviousWorkImage { ImageUrl = x }).ToList()
        };

        await _repo.AddAsync(work);
        await _repo.SaveChangesAsync();

        return Ok(work);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] PreviousWorkCreateUpdateDto dto)
    {
        if (dto.ImageUrls.Count > 5)
            return BadRequest(new { message = "Maximum 5 images allowed." });

        var existing = await _repo.GetByIdWithImagesAsync(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;

        existing.Images.Clear();
        foreach (var url in dto.ImageUrls)
            existing.Images.Add(new PreviousWorkImage { ImageUrl = url });

        _repo.Update(existing);
        await _repo.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repo.GetByIdWithImagesAsync(id);
        if (existing == null) return NotFound();

        _repo.Remove(existing);
        await _repo.SaveChangesAsync();

        return Ok(new { message = "Deleted successfully" });
    }
}