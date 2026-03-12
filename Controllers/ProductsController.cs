using AnnapurnaEnterprises.Api.DTOs;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnnapurnaEnterprises.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    // Public: list
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repo.GetAllWithImagesAsync();
        return Ok(products);
    }

    // Public: single
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _repo.GetByIdWithImagesAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    // Admin: create
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ProductCreateUpdateDto dto)
    {
        if (dto.ImageUrls.Count > 5)
            return BadRequest(new { message = "Maximum 5 images allowed." });

        var product = new Product
        {
            Name = dto.Name,
            PricePerSqFt = dto.PricePerSqFt,
            Description = dto.Description,
            Images = dto.ImageUrls.Select(x => new ProductImage { ImageUrl = x }).ToList()
        };

        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();

        return Ok(product);
    }

    // Admin: update
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductCreateUpdateDto dto)
    {
        if (dto.ImageUrls.Count > 5)
            return BadRequest(new { message = "Maximum 5 images allowed." });

        var existing = await _repo.GetByIdWithImagesAsync(id);
        if (existing == null) return NotFound();

        existing.Name = dto.Name;
        existing.PricePerSqFt = dto.PricePerSqFt;
        existing.Description = dto.Description;

        // replace images
        existing.Images.Clear();
        foreach (var url in dto.ImageUrls)
            existing.Images.Add(new ProductImage { ImageUrl = url });

        _repo.Update(existing);
        await _repo.SaveChangesAsync();

        return Ok(existing);
    }

    // Admin: delete
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