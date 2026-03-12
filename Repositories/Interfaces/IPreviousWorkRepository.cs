using AnnapurnaEnterprises.Api.Models;

namespace AnnapurnaEnterprises.Api.Repositories.Interfaces;

public interface IPreviousWorkRepository : IGenericRepository<PreviousWork>
{
    Task<List<PreviousWork>> GetAllWithImagesAsync();
    Task<PreviousWork?> GetByIdWithImagesAsync(int id);
}