using AnnapurnaEnterprises.Api.Data;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnnapurnaEnterprises.Api.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db) { }

    public async Task<List<Product>> GetAllWithImagesAsync()
    {
        return await _db.Products
            .Include(p => p.Images)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdWithImagesAsync(int id)
    {
        return await _db.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}