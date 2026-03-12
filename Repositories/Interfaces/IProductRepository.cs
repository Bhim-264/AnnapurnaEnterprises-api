using AnnapurnaEnterprises.Api.Models;

namespace AnnapurnaEnterprises.Api.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetAllWithImagesAsync();
    Task<Product?> GetByIdWithImagesAsync(int id);
}